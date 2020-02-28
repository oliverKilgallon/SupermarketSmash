using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainMenuManager : MonoBehaviour
{
    public string nextScene;
    public Canvas howToPlay;
    public GameObject sign;

    public void playGame()
    {
        SceneManager.LoadScene(nextScene);
    }
    public void HowToPlay()
    {
        howToPlay.gameObject.SetActive(true);
        sign.SetActive(false);
    }
    public void CloseHowToPlay()
    {
        howToPlay.gameObject.SetActive(false);
        sign.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "playGame")
                {
                    playGame();
                }
                if (hit.transform.gameObject.tag == "HTP")
                {
                    HowToPlay();
                }
                if (hit.transform.gameObject.tag == "exitGame")
                {
                    quit();
                }


            }
        }
    }



}
