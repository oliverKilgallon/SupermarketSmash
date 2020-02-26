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
        GetComponentInChildren<Renderer>().material.color = modelColour;
        characterModel.GetComponent<MeshFilter>().mesh = playerMesh;
    }
}
