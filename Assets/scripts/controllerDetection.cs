using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class controllerDetection : MonoBehaviour
{
    public GameObject[] playerPanelGameobjects;

    public int NoOfPlayers;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Controllers: "+Input.GetJoystickNames().Length);


        

    }

    // Update is called once per frame
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

        SceneManager.LoadScene("TomIngameUI");
    }


}
