using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerModelChanger : MonoBehaviour
{
    public Mesh playerMesh;
    public Color modelColour;
    public GameObject characterModel;

    // Start is called before the first frame update
    void Start()
    {
        playerModelExport pme = GameObject.FindGameObjectWithTag("playerModelExport").GetComponent<playerModelExport>();
        GameObject[] playerModelInfo = pme.playerPanels;
        GameObject player = playerModelInfo[GetComponentInChildren<MoveMultiplayer>().playerNumber - 1];
        modelColour = player.GetComponent<playerPanel>().playerColour.color;

        
        GetComponentInChildren<Renderer>().material.color = modelColour;
        characterModel.GetComponent<MeshFilter>().mesh = playerMesh;
    }
}
