using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public List<string> localItems = new List<string>();
    public Text[] listText = new Text[8];

    public List<string> currentHeld = new List<string>();
    ItemSpawn iS;

    public bool allItemsCollected = false;

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
            allItemsCollected = true;
        }
        else
        {
            allItemsCollected = false;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            if (currentHeld.Count > 0)
            {
                int index = Random.Range(0, currentHeld.Count - 1);
                string droppedItem = currentHeld[index];


                for (int i = 0; i < localItems.Count; i++)
                {
                    if (string.IsNullOrEmpty(localItems[i]))
                    {
                        localItems[i] = currentHeld[index];
                        GameObject dropped = iS.createItem(new Vector3(0,0,0), Random.Range(0, 360), currentHeld[index]);
                        dropped.GetComponent<Rigidbody>().AddForce(col.relativeVelocity * 10);
                        currentHeld.RemoveAt(index);
                        break;
                    }
                }
                //GameObject temp = iS.createItem(transform.position,transform.rotation.y, currentHeld[index]);
                
                //temp.GetComponent<Rigidbody>().AddForce(col.relativeVelocity);
               

            }
        }
    }


}
