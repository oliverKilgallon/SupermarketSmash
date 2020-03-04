using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public int playerNumber;
    public List<string> localItems = new List<string>();
    public List<string> AlllocalItems = new List<string>();//copy of local items at start which won't change to be a reference when things dissapeear from the origional
    public Text[] listText = new Text[8];

    public List<string> currentHeld = new List<string>();
    public string[] currentHeldWithPos = new string[8];
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
       
        foreach (string str in localItems) { AlllocalItems.Add(str); }
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
        /*
        int k = 0;
        foreach (string held in currentHeld)
        {
            int l = 0;
            foreach(string St in localItems)
            {
                if (AlllocalItems[l] == held&& string.IsNullOrEmpty(St))
                {


                    /* if (string.IsNullOrEmpty(localItems[l]))
                     {
                         if (basketListIcons[l].color == Color.clear)
                         {
                             //Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHH");
                             basketListIcons[l].color = Color.white;
                             foreach (Texture sprite in basketSpriteList)
                             {
                                 if (sprite.name == held) { basketListIcons[l].texture = sprite; }
                             }
                             l = 0;

                         }
                         break;
                     }
                     else { basketListIcons[l].color = Color.clear; }
                     l++;
                     
                }

            }

            k++;      
        } //*/

        
        /*
        int l = 0;
        int m = 0;
        foreach (RawImage basketIcon in basketListIcons)
        {
            //basketIcon.texture = null;
            //basketIcon.color = Color.clear;
        }
        foreach (string St in localItems)
        {
            if (string.IsNullOrEmpty(localItems[l]))
            {
                if (basketListIcons[l].color == Color.clear)
                {
                    foreach (Texture sprite in basketSpriteList)
                    {
                        if (sprite.name == currentHeld[m]) { basketListIcons[l].color = Color.white; basketListIcons[l].texture = sprite; }
                    }
                    //l = 0;

                    m++;
                }
            }
            else { basketListIcons[l].color = Color.clear; }
            l++;
        }//*/


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
                            int j = 0;
                            foreach(string held in AlllocalItems)
                            {
                                if (AlllocalItems[j] == droppedItem)
                                {
                                    break;
                                }
                                j++;
                            }
                            basketListIcons[j].texture = null;
                            basketListIcons[j].color = Color.clear;
                            localItems[j] = currentHeld[index];
                            GameObject temp = iS.createItem(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation.y, currentHeld[index]);
                            StartCoroutine(temp.GetComponent<ItemScript>().enableColliders(temp, .3f));
                            temp.GetComponent<Rigidbody>().AddForce((new Vector3(10, 1, 0) + col.relativeVelocity) * 50);
                            currentHeld.RemoveAt(index);
                            break;
                        }
                    }
                }
            }
            else { }
        }
    }

    public void ShoppingCartIcon(string held)
    {
        // basketListIcons[i].texture =
        //int m = 0;
       
        int j = 0;
        foreach(string heldAll in AlllocalItems)
        {
            if (held == heldAll&& basketListIcons[j].color == Color.clear)
            {
                foreach (Texture sprite in basketSpriteList)
                {
                    if (sprite.name == held)
                    {
                        basketListIcons[j].color = Color.white;
                        basketListIcons[j].texture = sprite;
                    }
                }
                break;

            } 

            j++;
        }
            
          
        
    }
    




}
