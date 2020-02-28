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
    public List<string> nameList;

    public string heldItem;//currently held item (For multiple collection, this will stay as the most recent)
    public List<string> heldItemList;

    public GameObject Spawn;//the object in the trolley to give the mesh and mat of the currently held object
    MeshRenderer spawnMeshR;//the mesh filter (model) component of /\
    MeshFilter spawnMeshF;//the mesh renderer (meterial) component of /\/\

    public bool allItemsCollected = false;
    public RawImage[] listIcons = new RawImage[8];
    public RawImage[] basketListIcons = new RawImage[8];
    public Texture[] basketSpriteList = new Texture[8];
    public string[] NamesList = {"Milk","Cola","Cereal","Pizza","Beans","Noodles","Bread","Butter"};

    // Start is called before the first frame update
    void Start()
    {
        //set Spawn's mesh components
        spawnMeshF = Spawn.GetComponent<MeshFilter>();
        spawnMeshR = Spawn.GetComponent<MeshRenderer>();

    
        nameList = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gameplayloop>().allItems;
        iS = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemSpawn>();
        playerNumber = GetComponent<MovementTest>().playerNumber;
        foreach(RawImage basketIcon in basketListIcons)
        {
            basketIcon.texture = null;
            basketIcon.color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*int p = 0;
        if(heldItem == "") { spawnMeshR.enabled = false; }//if we have no product, 
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

        }*/
        if (Input.GetButtonDown("joy" + playerNumber + "Throw"))
        {
            if (GetComponentInParent<powerupSlot>().current != null)
            {
                IEnumerator cr = GetComponentInParent<powerupSlot>().current.execEffect(GetComponentInParent<powerupSlot>().current.effectDuration, GetComponent<MovementTest>().playerNumber);
                StartCoroutine(cr);
                GetComponentInParent<powerupSlot>().removeItem();

            }
        }
        int k = 0;
        foreach (string held in heldItemList)
        {
            int l = 0;
            foreach(string St in localItems)
            {
                if (string.IsNullOrEmpty(localItems[l]))
                {
                    basketListIcons[l].color = Color.clear;
                }
                l++;
            }

            k++;      
        } 

        int i = 0;
        int left = 8;
        
        foreach (Text t in listText)
        {
            
            if(localItems[i]== "" || localItems[i] == null)
            {
                listIcons[i].color = Color.clear; ;
                t.text = "";
                left--;
            }
            else
            {
                t.text = localItems[i];
                //listIcons[i].texture = iS.spriteList[i];
                int j = 0;
                foreach(Texture texture in iS.spriteList)
                {
                    if (localItems[i] == NamesList[j])
                    {
                        listIcons[i].color = Color.white;
                        listIcons[i].texture = texture;
                        
                    }
                    j++;
                }
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
            if (GetComponent<Rigidbody>().velocity.magnitude < col.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
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
                            temp.GetComponent<Rigidbody>().AddForce((new Vector3(1, 1, 1) + col.relativeVelocity) * 50);
                            currentHeld.RemoveAt(index);
                            break;
                        }
                    }
                }
            }
            else { }
        }
    }

    




}
