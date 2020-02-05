using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public int playerNumber;
    public List<string> localItems = new List<string>();
    public Text[] listText = new Text[8];

    public List<string> currentHeld = new List<string>();
    ItemSpawn iS;

    public int armour;
    public string heldItem;
    public List<string> nameList;
    public GameObject Spawn;
    MeshRenderer spawnMeshR;
    MeshFilter spawnMeshF;

    public bool allItemsCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnMeshF = Spawn.GetComponent<MeshFilter>();
        spawnMeshR = Spawn.GetComponent<MeshRenderer>();
        nameList = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gameplayloop>().allItems;
        iS = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemSpawn>();
        playerNumber = GetComponent<MoveMultiplayer>().playerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        int p = 0;
        if(heldItem == "") { spawnMeshR.enabled = false; }
        if(heldItem != null && heldItem!= "")
        {
            foreach(string name in nameList)
            {
          
                if (name == heldItem)
                {
                    //iS.productMat
                    spawnMeshF.mesh = iS.productMesh[p];
                    spawnMeshR.material = iS.productMat[p];
                    spawnMeshR.enabled = true;


                }
                p++;  
            }

        }
        if (Input.GetButtonDown("joy" + playerNumber + "Throw"))
        {
            if (GetComponentInParent<powerupSlot>().current != null)
            {
                IEnumerator cr = GetComponentInParent<powerupSlot>().current.execEffect(GetComponentInParent<powerupSlot>().current.effectDuration, GetComponent<MoveMultiplayer>().playerNumber);
                StartCoroutine(cr);
                GetComponentInParent<powerupSlot>().current = null;
                GetComponentInParent<powerupSlot>().slot.texture = null;
            }
        }
        */
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
                        GameObject temp = iS.createItem(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation.y, currentHeld[index]);
                        StartCoroutine(temp.GetComponent<ItemScript>().enableColliders(temp, .3f));
                        temp.GetComponent<Rigidbody>().AddForce((new Vector3(1,1,1) + col.relativeVelocity) * 50);
                        currentHeld.RemoveAt(index);
                        break;
                    }
                }
            }
        }
    }

    




}
