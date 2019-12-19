using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class controllerDetection : MonoBehaviour
{
    public GameObject[] playerPanelGameobjects;

    public int NoOfPlayers;

    
    void Start()
    {
        Debug.Log("Controllers: " + Input.GetJoystickNames().Length);
    }
    void Update()
    {

        int i = 0;
        foreach (GameObject pp in playerPanelGameobjects)
        {
            if (i < Input.GetJoystickNames().Length)
            {
                pp.SetActive(true);
                i++;
            }
            else { break; }
        }
        NoOfPlayers = Input.GetJoystickNames().Length;
        i = 0;
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("NoOfPlayers", NoOfPlayers);

        SceneManager.LoadScene("playerCollision");
    }
    public void back()
    {
        PlayerPrefs.SetInt("NoOfPlayers", 0);

        SceneManager.LoadScene("mainMenu");
    }

}
