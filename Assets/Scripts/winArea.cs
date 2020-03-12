using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class winArea : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject[] winScreenPlayerLocations;
    public GameObject winScreenCamera;
    public GameObject[] lists;

    Gameplayloop gc;

    //private List<int> playerScores = new List<int>();
    List<playerScore> playerScores = new List<playerScore>();


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

    playerScore getPlayerScore(GameObject player)
    {
        playerScore newPlayerScore = new playerScore(0, player.GetComponent<MovementTest>().playerNumber - 1);

        foreach (string item in player.GetComponentInChildren<Playerscript>().currentHeld)
        {
            if (!string.IsNullOrEmpty(item))
            {
                newPlayerScore.score++;
            }
        }
        foreach (string item in player.GetComponentInChildren<Playerscript>().localItems)
        {
            if (!string.IsNullOrEmpty(item))
            {
                newPlayerScore.score++;
            }
        }
        return newPlayerScore;
    }


    void setupEndScreen(GameObject winner)
    {
        //winner.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //winner.GetComponentInChildren<MovementTest>().angularVelocity = 0f;
        //winner.GetComponentInChildren<MovementTest>().animator.SetFloat("ForwardSpeed", 0f);
        foreach (GameObject player in gc.players)
        {
            playerScores.Add(getPlayerScore(player));
            player.GetComponent<MovementTest>().enabled = false;
            player.transform.parent.GetComponentInChildren<Camera>().enabled = false;
        }
        playerScores.Sort();
        Debug.Log(listToString(playerScores));

        moveToPodium(0, playerScores[0].playerNumber);
        

        foreach (GameObject list in lists)
        {
            list.SetActive(false);
        }
        winScreenCamera.SetActive(true);

        //winner.transform.position = winScreenPlayerLocation.transform.position;
        //winner.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        
        winScreen.SetActive(true);
    }
   

    void moveToPodium(int pos, int player)
    {
        gc.players[player].GetComponentInChildren<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gc.players[player].GetComponentInChildren<MovementTest>().angularVelocity = 0f;
        gc.players[player].GetComponentInChildren<MovementTest>().animator.SetFloat("ForwardSpeed", 0f);
        gc.players[player].transform.position = winScreenPlayerLocations[pos].transform.position;
        gc.players[player].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }


    string listToString(List<playerScore> input)
    {
        string list = "";
        foreach(playerScore i in input)
        {
            list += "Player "+ i.playerNumber + ": " + i.score + ", ";
        }
        return list;
    }
}
class playerScore : IComparable<playerScore>
{
    public Int32 score { get; set; }
    public Int32 playerNumber { get; set; }

    public playerScore(Int32 score, Int32 playerNo)
    {
        this.score = score;
        this.playerNumber = playerNo;
    }

    public Int32 CompareTo(playerScore other)
    {
        return score.CompareTo(other.score);
    }
}