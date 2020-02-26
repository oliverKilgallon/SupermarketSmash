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
        Debug.Log(joystickList());
        int i = 0;
        NoOfPlayers = getPlayers();
        foreach (GameObject pp in playerPanelGameobjects)
        {
            if (i < NoOfPlayers)
            {
                pp.SetActive(true);  
            }
            else 
            {
                pp.SetActive(false);     
            }
            i++;
        }
        i = 0;
    }

    int getPlayers()
    {
        string[] joysticks = Input.GetJoystickNames();
        NoOfPlayers = 0;
        for (int i = 0; i < joysticks.Length; i++)
        {
            if (joysticks[i] == "") { }
            else
            {
                NoOfPlayers++;
            }
        }
        return NoOfPlayers;
    }

    string joystickList()
    {
        string list = "";

        foreach (string js in Input.GetJoystickNames())
        {
            list += js + ", ";
        }

        return list;
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
