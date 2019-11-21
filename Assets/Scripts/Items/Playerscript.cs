﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public List<string> localItems = new List<string>();
    public Text[] listText = new Text[8];

    public List<string> currentHeld = new List<string>();
    ItemSpawn iS;

    // Start is called before the first frame update
    void Start()
    {
        iS = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        int left = 8;
        foreach (Text t in listText)
        {
            if(localItems[i]== "" || localItems[i] == null)
            {
                t.text = "";
                left--;
            }
            else
            {
                t.text = localItems[i];
            }


            i++;
        }
        if (left == 0)
        {

            Debug.Log("your winner"); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (currentHeld.Count > 0)
            {
                int index = Random.Range(0, currentHeld.Count - 1);
                string droppedItem = currentHeld[index];


                for (int i = 0; i < localItems.Count; i++)
                {
                    if (string.IsNullOrEmpty(localItems[i]))
                    {
                        localItems[i]= currentHeld[index];
                        break;
                    }
                }
                GameObject temp = iS.createItem(transform.position,transform.rotation.y, currentHeld[index]);
                currentHeld.RemoveAt(index);
                temp.GetComponent<Rigidbody>().AddForce(collision.relativeVelocity);
               

            }
        }
    }


}
