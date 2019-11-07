using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplayloop : MonoBehaviour
{
    public List<string> allItems = new List<string>();
    public List<string> allPlayersItems = new List<string>();
    public GameObject[] players;
    public GameObject splitscreen;
    public int listLength;
    public int sharedListLength;
    public int noOfPlayers;
    public bool setListMode;
    // Start is called before the first frame update
    void Awake()
    {
        noOfPlayers = splitscreen.gameObject.GetComponent<setUpSplitScreen>().numberOfPlayers;
       // Debug.Log("test2");
        int counter = 0;
        string line;
        System.IO.StreamReader file =
           new System.IO.StreamReader("Assets/ItemStorage.txt");
        while ((line = file.ReadLine()) != null)
        {
           // Debug.Log(line);
            allItems.Add(line);
            counter++;
        }
        file.Close();
        if (setListMode == true)
        {
            int randomProduct = Random.Range(0, allItems.Count);
            int donePlayers = 0;
            List<string> playerList = new List<string>();
            for (int i = 0; i < listLength; i++)
            {
                playerList.Add(allItems[Random.Range(0, allItems.Count )]);


            }
            foreach (GameObject player in players)
            {

                if (donePlayers < noOfPlayers)
                {
                    player.GetComponent<Playerscript>().localItems = playerList;
                    if (donePlayers < noOfPlayers/2)//if using the same list for everyone, we need half the items 
                    {
                        foreach (string item in playerList)
                        {
                            allPlayersItems.Add(item);
                        }
                    }
                    donePlayers++;
                }
            }
        }//give the players a set shopping list
        else
        {
            int randomProduct = Random.Range(0, allItems.Count);
            int donePlayers = 0;
            List<string> playerList = new List<string>();
            for (int i = 0; i < sharedListLength; i++)
            { 
                playerList.Add(allItems[Random.Range(0, allItems.Count)]);
            }
           foreach (GameObject player in players)
            {
                
                if (donePlayers < noOfPlayers)
                {
                    Debug.Log("playerListLength: " + playerList.Count);

                    List<string> thisPlayerList = player.GetComponent<Playerscript>().localItems;
                    thisPlayerList.AddRange(playerList);
                    for (int i = 0; i < (listLength - sharedListLength); i++)
                    {
                        //Debug.Log("test");
                        thisPlayerList.Add(allItems[Random.Range(0, allItems.Count)]);
                        

                    }
                    
                    foreach (string item in thisPlayerList)
                    {
                        allPlayersItems.Add(item);
                    }
                    

                    donePlayers++;
                }
            }
        }//give the players some set and some different shopping list items
    }    


    // Update is called once per frame
    void Update()
    {
        
    }
}
