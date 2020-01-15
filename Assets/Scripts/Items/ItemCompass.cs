using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCompass : MonoBehaviour
{
    //To access all items in the scene
    public GameObject productManager;

    //To access the player's item list
    public Playerscript playerScript;

    public int minDetectionRadius = 20;

    //How long the item checker should wait between checks (in seconds)
    public int checkFrequency = 5;

    //List that will contain all shopping items, later will check collected status
    private List<Transform> ItemTransformList;
    
    //Nearest item transform among all the items in the item list
    private Transform nearestItemTransform;

    //Default transform to use in the event that the item list is empty or finished
    private Transform defaultTransform;

    //Relative distance between the nearest item and the flag in 2 dimensions
    private Vector2 relativePosVec2;

    //The position of the nearest item
    private Vector3 relativeItemPos;

    //The rotation that will be applied to the flag
    private Quaternion rotation;

    private void Start()
    {
        productManager = GameObject.Find("ProductManager");

        //Init default transform to point straight forward
        defaultTransform = transform;

        nearestItemTransform = defaultTransform;

        ItemTransformList = new List<Transform>();

        //Populate list
        foreach (Transform child in productManager.transform)
        {
            ItemTransformList.Add(child);
        }
    }

    void Update()
    {
        //If there are items in the scene, find the nearest one, else use the default transform
        if (ItemTransformList.Count <= 0)
        {
            nearestItemTransform = defaultTransform;
        }

        StartCoroutine(FindNearestObject());
        //Set rotation to look at nearest transform, zeroing out y rotation along the way
        LookAtNearestItem();
    }

    IEnumerator FindNearestObject()
    {
        /**
         * Clear the transform list and re-populate to make sure collected items aren't 
         * still being checked if some are collected
        */
        ItemTransformList.Clear();
        foreach (Transform child in productManager.transform)
        {
            ItemTransformList.Add(child);
        }

        foreach (Transform child in productManager.transform)
        {
            float distance = Mathf.Infinity;

            relativeItemPos = child.transform.position - transform.position;

            //Only look at the item if it is inside the detection radius and is the closest item among those
            if ((relativeItemPos.sqrMagnitude < minDetectionRadius) && (relativeItemPos.sqrMagnitude < distance))
            {
                foreach (string playerItem in playerScript.localItems)
                {
                    //Make sure the item is actually on the player's shopping list currently
                    if (child.GetComponent<ItemScript>().product.Equals(playerItem))
                    {
                        nearestItemTransform = child;
                        distance = relativeItemPos.sqrMagnitude;
                    }
                }
            }
        }

        yield return new WaitForSeconds(5);
    }

    private void LookAtNearestItem()
    {
        //Vector3 lookTransform = new Vector3(nearestItemTransform.position.x, Vector3.Angle(nearestItemTransform.position, transform.position) , nearestItemTransform.position.z);
        //transform.LookAt(nearestItemTransform, Vector3.up);

        transform.rotation = Quaternion.LookRotation(nearestItemTransform.position - transform.position, Vector3.up);

        Vector3 transformEuler = transform.rotation.eulerAngles;
        transformEuler = new Vector3(0, transformEuler.y, 0);

        transform.rotation = Quaternion.Euler(transformEuler);
        /*
        relativeItemPos = nearestItemTransform.position - transform.position;

        //relativeItemPos.y = 0f;

        rotation = Quaternion.LookRotation(relativeItemPos, Vector3.up);

        transform.rotation = rotation;
        */
    }
}
