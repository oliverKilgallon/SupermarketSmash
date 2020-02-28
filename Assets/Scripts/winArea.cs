using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winArea : MonoBehaviour
{
    public GameObject yourWinner;
    public GameObject winScreenPlayerLocation;
    public GameObject winScreenCamera;
    public GameObject[] lists;

    Gameplayloop gc;

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<Gameplayloop>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            if (listIsEmpty(other.GetComponent<Playerscript>().localItems) && listIsEmpty(other.GetComponent<Playerscript>().currentHeld))
            {
                setupEndScreen(other.gameObject);
            }
            else {}  
        }

    }

    bool listIsEmpty(List<string> itemList)
    {
        foreach (string item in itemList)
        {
            if (string.IsNullOrEmpty(item)){}
            else{return false;}
        }
        return true;
    }

    void setupEndScreen(GameObject winner)
    {
        winner.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0, 0, 0);
        winner.GetComponentInChildren<MovementTest>().angularVelocity = 0f;
        winner.GetComponentInChildren<MovementTest>().animator.SetFloat("ForwardSpeed", 0f);
        foreach (GameObject player in gc.players)
        {
            player.GetComponent<MovementTest>().enabled = false;
            player.transform.parent.GetComponentInChildren<Camera>().enabled = false;
        }
        foreach (GameObject list in lists)
        {
            list.SetActive(false);
        }
        winScreenCamera.SetActive(true);

        


        winner.transform.position = winScreenPlayerLocation.transform.position;
        winner.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        

        yourWinner.SetActive(true);
    }


}
