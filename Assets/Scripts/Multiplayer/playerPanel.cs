using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerPanel : MonoBehaviour
{
    public int playerJoystickNumber;

    public string playerName; 
    public Mesh mesh;
    public Text playerNumber;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerJoystickNumber = -1;
    }
    private void Update()
    {
        playerNumber.text = "" + playerJoystickNumber;
        mesh = GetComponentInChildren<modelPicker>().TEST.GetComponent<MeshFilter>().mesh;
    }

}
