using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainMenuManager : MonoBehaviour
{
    public string nextScene;
    public string optionsScene;

    public void playGame()
    {
        SceneManager.LoadScene(nextScene);
    }
    public void options()
    {

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
                if (hit.transform.gameObject.tag == "options")
                {

                }
                if (hit.transform.gameObject.tag == "exitGame")
                {
                    quit();
                }


            }
        }
    }



}
