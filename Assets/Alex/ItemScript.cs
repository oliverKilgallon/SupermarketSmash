using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public string product;
    public bool inWall;
   
    // Use this for initialization
    void Start()
    {
        inWall = false;
       // product = Random.Range(0, 15);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            inWall = true;
        }

    }
    /*void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            inWall = true;
        }
    }*/
}
