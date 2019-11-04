using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawn : MonoBehaviour
{
    public GameObject[] pointArray;
    public GameObject[] sphereArray;
    public GameObject spawnable;
   
    
   
   

    public float onMeshThreshold;
    int stoppedCount;
    // public GameObject[] items;
    public List<GameObject> items = new List<GameObject>();
    int itemArrayCount;

    // Start is called before the first frame update
    void Start()
    {
        itemArrayCount = 0;
        stoppedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int randPoint;
        Vector3 randLocation;
        for(int i = 0;i<10 ;i++ )
        if (Input.GetKeyDown("o")) {
            randPoint = Random.Range(0,pointArray.Length);
            randLocation = new Vector3(pointArray[randPoint].transform.position.x,2, pointArray[randPoint].transform.position.z);
                SpawnRandom(randLocation,randPoint);
                //Instantiate(spawnable, randLocation, Quaternion.Euler(new Vector3(0, randAngle, 0)));
               
        }
        
    }
    public void SpawnRandom(Vector3 spawnLocation,int point)
    {
        float randLength;
        float randAngle;
       
        randAngle = Random.Range(0, 360);
        randLength = Random.Range(0, sphereArray[point].transform.lossyScale.x / 2);
        var q = Quaternion.AngleAxis(randAngle, Vector3.up);
        spawnLocation = spawnLocation + q * Vector3.left * randLength;
        // randLocation = randLocation + (new Vector3(0, randAngle, 0) * randLength);
        randAngle = Random.Range(0, 360);
        
        //items[itemArrayCount] = thisProduct;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(new Vector3(spawnLocation.x, spawnLocation.y + 10, spawnLocation.z), Vector3.down, out hit, Mathf.Infinity))
        {
            
           
            if (hit.collider.tag == "wall")
            {
                Debug.DrawRay(new Vector3(spawnLocation.x, spawnLocation.y + 10, spawnLocation.z), Vector3.down * hit.distance, Color.red, 5.0f);
                Debug.Log("Did Hit");
                SpawnRandom(spawnLocation, point);
            }
            else
            {
                Debug.DrawRay(new Vector3(spawnLocation.x, spawnLocation.y + 10, spawnLocation.z), Vector3.down * hit.distance, Color.white, 5.0f);
                GameObject thisProduct;
                thisProduct = (GameObject)Instantiate(spawnable, spawnLocation, Quaternion.Euler(new Vector3(0, randAngle, 0)));
            }
        }
        else
        {
            
           // Debug.Log("Did not Hit");
        }

      
    }
  
   
}
