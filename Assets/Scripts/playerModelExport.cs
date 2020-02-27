using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerModelExport : MonoBehaviour
{
    public GameObject[] playerPanels;
    public int noOfPlayers;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
