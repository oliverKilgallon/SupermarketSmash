using UnityEngine;

public class ItemCompass : MonoBehaviour
{

    //List that will contain all shopping items, later will check collected status
    public Transform[] ItemTransformList;
    
    //Nearest item transform among all the items in the item list
    private Transform nearestItemTransform;

    //Default transform to use in the event that the item list is empty or finished
    private Transform defLookTransform;

    //Relative distance between the nearest item and the flag
    private Vector3 relativePos;

    //The rotation that will be applied to the flag
    private Quaternion rotation;

    private void Start()
    {
        //Init default transform to point straight forward
        defLookTransform = transform;
    }

    void Update()
    {
        /**Set init nearest transform to first item in list if the list has at least 1 item
         * else set init transform to default
         */
        if (ItemTransformList.Length > 0)
        {
            nearestItemTransform = ItemTransformList[0];
        }

        else
        {
            nearestItemTransform = defLookTransform;
        }

        //Iterate through itemlist then set nearest transform to the smallest distance among the items
        for (int i = 0; i < ItemTransformList.Length; i++)
        {
            if (ItemTransformList[i].position.magnitude < nearestItemTransform.position.magnitude)
            {
                nearestItemTransform = ItemTransformList[i];
            }
        }

    //Set rotation to look at nearest transform, zeroing out y rotation along the way
    transform.LookAt(nearestItemTransform, Vector3.up);
    
    relativePos = nearestItemTransform.position - transform.position;
    
    relativePos.y = 0f;
    
    rotation = Quaternion.LookRotation(relativePos, Vector3.up);

    transform.rotation = rotation;
    }
}
