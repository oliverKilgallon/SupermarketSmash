using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerPanel : MonoBehaviour
{
    public int playerJoystickNumber;

    public string playerName;
    public GameObject playerModel;
    public Text playerNumber;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerJoystickNumber = -1;
    }
    private void Update()
    {
        playerNumber.text = "" + playerJoystickNumber;
        playerModel = GetComponentInChildren<modelPicker>().Display;
    }

}
