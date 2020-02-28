using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class controllerDetection : MonoBehaviour
{
    public GameObject[] playerPanelGameobjects;
    public LevelLoader levelLoader;

    public Canvas canvas;

    public int NoOfPlayers;
    public int HostController;

    public Object nextScene;
    public Object lastScene;

    void Start()
    {
        //Debug.Log("Controllers: " + Input.GetJoystickNames().Length);
        foreach (GameObject panel in playerPanelGameobjects)
        {
            panel.SetActive(false);
        }
    }
    void Update()
    {
        getHostController();
        //joystickList();
        int i = 0;
        NoOfPlayers = getPlayers();
        //Debug.Log(NoOfPlayers);
        if (NoOfPlayers < 2)
        {
            PlayerPrefs.SetInt("NumberOfPlayers", 2);
            NoOfPlayers = 2;
        }
        else
        {
            PlayerPrefs.SetInt("NumberOfPlayers", NoOfPlayers);
        }
        //Debug.Log(PlayerPrefs.GetInt("NumberOfPlayers"));
        foreach (GameObject pp in playerPanelGameobjects)
        {
            if (i < NoOfPlayers)
            {
                pp.SetActive(true);
                if (pp.GetComponent<playerPanel>().playerJoystickNumber == -1)
                {
                    pp.GetComponent<playerPanel>().playerJoystickNumber = (i + 1);
                }
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
        //Debug.Log(NoOfPlayers);
        return NoOfPlayers;
    }

    string joystickList()
    {
        string list = "";

        foreach (string js in Input.GetJoystickNames())
        {
            list += js + ", ";
        }
        Debug.Log(list);
        return list;
    }
    int getHostController()
    {
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (!string.IsNullOrEmpty( Input.GetJoystickNames()[i]))
            {
                HostController = i;
                break;
            }else{}
        }
        Debug.Log(HostController);

        return HostController;
    }


    public void Continue()
    {
        PlayerPrefs.SetInt("NoOfPlayers", NoOfPlayers);
        canvas.GetComponent<Canvas>().enabled = false;
        levelLoader.LoadNextLevel(nextScene);
    }
    public void back()
    {
        Destroy(canvas.gameObject);
        Destroy(GameObject.Find("playerModelExport"));
        PlayerPrefs.SetInt("NoOfPlayers", 0);
        levelLoader.LoadLastLevel(lastScene);        
    }
}
