using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMultiplayer : MonoBehaviour
{
    public int playerNumber;

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
    Playerscript ps;
    

    // Start is called before the first frame update
    void Start()
    {
       ps = GetComponent<Playerscript>();
      

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("joy"+playerNumber+"Acc")) { ctrlA = true; }if (Input.GetKeyUp("joy"+playerNumber+"Acc")) { ctrlA = false; }
        if (Input.GetKeyDown("joystick " + playerNumber + " button 0")) { ctrlA = true; }
        if (Input.GetKeyUp("joystick " + playerNumber + " button 0")) { ctrlA = false; }
        if (Input.GetKeyDown("q")) { qPress = true; }if (Input.GetKeyUp("q")) { qPress = false; }
        if (Input.GetKeyDown("w")) { wPress = true; }if (Input.GetKeyUp("w")) { wPress = false; }
        if (Input.GetKeyDown("e")) { ePress = true; }if (Input.GetKeyUp("e")) { ePress = false; }
        // if (Input.GetAxis("Vertical"))

        //Debug.Log("Player number: " + playerNumber + "'s x axis: " + Input.GetAxisRaw("joy" + playerNumber + "x"));

        //Check if x axis of the left joystick of this player isn't zero then calculate Angle by multiplying the turnSpeed into the input
        if (Input.GetAxis("joy" + playerNumber + "x") != 0)
        {
            Angle = new Vector3(0, Input.GetAxis("joy" + playerNumber + "x") * turnSpeed, 0); }
        else
        { Angle = new Vector3(0, 0, 0); }
        
        // body.AddForceAtPosition(transform.rotation.y * turnSpeed,body.gameObject.transform.position);

        // body.AddForce(transform.forward * Input.GetAxis("Thrust")); 
    }
    void FixedUpdate()
    {
       
        if (ctrlA||wPress) { body.AddForce(transform.forward * thrust); }


         
        Quaternion deltaRotation = Quaternion.Euler(Angle * Time.deltaTime);
        
        //body.MoveRotation(body.rotation * deltaRotation);
        //body.AddTorque(transform.up * turnSpeed * ((body.rotation * deltaRotation).eulerAngles));
            body.AddTorque(transform.up * turnSpeed * Input.GetAxis("joy" + playerNumber + "x"));
    }



    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "item")
        {
            int i = 0;
            foreach (string item in ps.localItems)
            {
                if (ps.localItems[i] != "" && ps.localItems[i] != null)
                {
                    Debug.Log(item + " " + col.gameObject.GetComponent<ItemScript>().product);
                    if (item == col.gameObject.GetComponent<ItemScript>().product)
                    {

                        ps.listText[ps.localItems.IndexOf(item)].text = "";
                        ps.localItems[ps.localItems.IndexOf(item)] = "";
                        ps.currentHeld.Add(item);
                        Destroy(col.gameObject);
                        break;

                    }

                }
                i++;
            }

            //string product = col.gameObject.GetComponent<ItemScript>().product;
            //control.gameObject.GetComponent<ItemSpawn>().;
            // Debug.Log("product "+ product);


        }
    }
}
