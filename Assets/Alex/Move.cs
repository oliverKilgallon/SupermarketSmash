﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody body;
    public GameObject Camera; 
    public float thrust;
    public float turnSpeed;
    public bool qPress;
    public bool wPress;
    public bool ePress;
    public bool ctrlA;
    
    public Vector3 Angle;
    public GameObject control;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0")) { ctrlA = true; }if (Input.GetKeyUp("joystick button 0")) { ctrlA = false; }
        if (Input.GetKeyDown("q")) { qPress = true; }if (Input.GetKeyUp("q")) { qPress = false; }
        if (Input.GetKeyDown("w")) { wPress = true; }if (Input.GetKeyUp("w")) { wPress = false; }
        if (Input.GetKeyDown("e")) { ePress = true; }if (Input.GetKeyUp("e")) { ePress = false; }
        // if (Input.GetAxis("Vertical"))
        
        if (Input.GetAxis("Horizontal") != 0)
        { Angle = new Vector3(0, Input.GetAxis("Horizontal") * turnSpeed, 0); }
        else
        { Angle = new Vector3(0, 0, 0); }
        
        // body.AddForceAtPosition(transform.rotation.y * turnSpeed,body.gameObject.transform.position);

        // body.AddForce(transform.forward * Input.GetAxis("Thrust")); 
    }
    void FixedUpdate()
    {
         
       
        if (ctrlA||wPress) { body.AddForce(transform.forward * thrust); }


         
        Quaternion deltaRotation = Quaternion.Euler(Angle * Time.deltaTime);
        
        body.MoveRotation(body.rotation * deltaRotation);
        
      
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "item")
        {
            int product = col.gameObject.GetComponent<ItemScript>().product;
            //control.gameObject.GetComponent<ItemSpawn>().;
            Debug.Log("product "+ product);
            Destroy(col.gameObject);
            
        }
    }
}
