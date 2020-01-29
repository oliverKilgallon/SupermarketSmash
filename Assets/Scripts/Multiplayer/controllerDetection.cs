using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class controllerDetection : MonoBehaviour
{
    public GameObject[] playerPanelGameobjects;

    public int NoOfPlayers;

    public Object nextScene;
    public Object lastScene;
    
    void Start()
    {
        Debug.Log("Controllers: " + Input.GetJoystickNames().Length);
        foreach (GameObject panel in playerPanelGameobjects)
        {
            panel.SetActive(false);
        }
    }
    void Update()
    {
        int i = 0;
        foreach (GameObject pp in playerPanelGameobjects)
        {
            if (i < NoOfPlayers)
            {
                pp.SetActive(true);
                i++;
            }
            else 
            {
                pp.SetActive(false);
                i++;
            }
        }
        NoOfPlayers = Input.GetJoystickNames().Length;
        i = 0;
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("NoOfPlayers", NoOfPlayers);

        SceneManager.LoadScene(nextScene.name);
    }
    public void back()
    {
        PlayerPrefs.SetInt("NoOfPlayers", 0);

        SceneManager.LoadScene(lastScene.name);
    }
}
