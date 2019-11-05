using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainMenuManager : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("playerReadyUp");
    }
    public void options()
    {

    }
    public void quit()
    {
        Application.Quit();
    }
}
