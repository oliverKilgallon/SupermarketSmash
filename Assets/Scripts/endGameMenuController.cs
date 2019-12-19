using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class endGameMenuController : MonoBehaviour
{
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
