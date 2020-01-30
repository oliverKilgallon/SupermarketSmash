using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawn : MonoBehaviour
{
    public Mesh[] productMesh;
    public Material[] productMat;
    public GameObject[] pointArray;
    public GameObject[] sphereArray;
    public GameObject spawnable;
    public GameObject ProductParent;
    public List<string> allItems;

    public bool spawntest;
    public Dictionary<string,Mesh> itemhashMesh = new Dictionary<string,Mesh>();
    public Dictionary<string,Material> itemhashMat = new Dictionary<string,Material>();
    public Material test;

    public float onMeshThreshold;
    //int stoppedCount;
    // public GameObject[] items;
    public List<GameObject> items = new List<GameObject>();
    //int itemArrayCount;


    // Start is called before the first frame update
    void Start()
    {
        allItems = this.gameObject.GetComponent<Gameplayloop>().allItems;
        int i = 0;
        foreach(string item in allItems)
        {
            itemhashMesh.Add(item, productMesh[i]);
            itemhashMat.Add(item, productMat[i]);
            i++;
        }
        //pMeshHash.Add("Cereal", test);
       // Debug.Log("test1");
        //itemArrayCount = 0;
        //stoppedCount = 0;
        
        foreach (string item in this.gameObject.GetComponent<Gameplayloop>().allPlayersItems)
        {
            //Debug.Log(item);
            int pointCount = 0;
            foreach (GameObject point in pointArray)
            {
               // Debug.Log(item);
                int count = 0;
                foreach (string pointitem in point.gameObject.GetComponent<PointList>().acceptedItems)
                {
                    if (item == point.gameObject.GetComponent<PointList>().acceptedItems[count])
                    {
                        SpawnRandom(new Vector3(point.transform.position.x,2,point.transform.position.z), pointCount, item);


                    }
                    count++;
                }
                pointCount++;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        int randPoint;
        Vector3 randLocation;
        for(int i = 0;i<10 ;i++ )
        if (Input.GetKeyDown("o")&&spawntest) {
            randPoint = Random.Range(0,pointArray.Length);
            randLocation = new Vector3(pointArray[randPoint].transform.position.x,2, pointArray[randPoint].transform.position.z);
                SpawnRandom(randLocation,randPoint,"test");
                //Instantiate(spawnable, randLocation, Quaternion.Euler(new Vector3(0, randAngle, 0)));
               
        }
        
    }
    public void SpawnRandom(Vector3 spawnLocation,int point,string item)
    {
        float randLength;
        float randAngle;
        Vector3 oldLocation;
        oldLocation = spawnLocation;
        randAngle = Random.Range(0, 360);
        randLength = Random.Range(0, sphereArray[point].transform.lossyScale.x / 2);
        var q = Quaternion.AngleAxis(randAngle, Vector3.up);
        spawnLocation = spawnLocation + q * Vector3.left * randLength;
        // randLocation = randLocation + (new Vector3(0, randAngle, 0) * randLength);
        randAngle = Random.Range(0, 360);
        
        //items[itemArrayCount] = thisProduct;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(new Vector3(spawnLocation.x, spawnLocation.y + 10, spawnLocation.z), Vector3.down, out hit,200))
        {
            
           
            if (hit.collider.tag != "floor")
            {
               // Debug.Log("missed");
                Debug.DrawRay(new Vector3(spawnLocation.x, spawnLocation.y + 10, spawnLocation.z), Vector3.down * hit.distance, Color.red, 5.0f);
               // Debug.Log("Did Hit");
                SpawnRandom(oldLocation, point,item);
            }
            else
            {
                Debug.DrawRay(new Vector3(spawnLocation.x, spawnLocation.y + 10, spawnLocation.z), Vector3.down * hit.distance, Color.white, 5.0f);
                GameObject newItem = createItem(spawnLocation, randAngle, item);
                StartCoroutine(newItem.GetComponent<ItemScript>().enableColliders(newItem, .1f));
            }
        }
        else
        {
            
           // Debug.Log("Did not Hit");
        }

      
    }


    public GameObject createItem(Vector3 spawnLoc, float randAngle, string item)
    {
        GameObject thisProduct;
        thisProduct = (GameObject)Instantiate(spawnable, spawnLoc, Quaternion.Euler(new Vector3(0, randAngle, 0)));
        thisProduct.gameObject.GetComponent<ItemScript>().product = item;
        thisProduct.gameObject.GetComponent<ItemScript>().itemMat = itemhashMat[item];
        thisProduct.gameObject.GetComponent<ItemScript>().itemMesh = itemhashMesh[item];
        thisProduct.gameObject.GetComponent<MeshFilter>().mesh = thisProduct.gameObject.GetComponent<ItemScript>().itemMesh;
        thisProduct.gameObject.GetComponent<MeshCollider>().sharedMesh = thisProduct.gameObject.GetComponent<ItemScript>().itemMesh;
        thisProduct.gameObject.GetComponent<MeshRenderer>().material = thisProduct.gameObject.GetComponent<ItemScript>().itemMat;

        thisProduct.transform.parent = ProductParent.transform;
        return thisProduct;
    }
}
