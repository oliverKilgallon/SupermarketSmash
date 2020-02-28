using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptions : MonoBehaviour
{
    public string nextScene;
    public Canvas howToPlay;
    public GameObject sign;

    public void PlayGame()
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
    public void Quit()
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
                    PlayGame();
                }
                if (hit.transform.gameObject.tag == "HTP")
                {
                    HowToPlay();
                }
                if (hit.transform.gameObject.tag == "exitGame")
                {
                    Quit();
                }


            }
        }
    }
}
