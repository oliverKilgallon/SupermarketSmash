using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerDetection : MonoBehaviour
{
    public GameObject[] playerPanelGameobjects;
    //playerPanel[] playerPanels = new playerPanel[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("joy" + i + "start"))
            {
                Debug.Log("joy" + i + "start");
                playerPanel temp = firstAvailable();
                temp.controllerConnected = true;
                temp.panel.enabled = true;
            }
        }
    }

    playerPanel firstAvailable()
    {
        playerPanel firstAv = null;

        for(int i = 0;i<4;i++)
        {
            Debug.Log(i);
            playerPanel pp = playerPanelGameobjects[i].GetComponent<playerPanel>();

            if (!pp.controllerConnected)
            {
                firstAv = pp;
            }
        }

        return firstAv;
    }
}
