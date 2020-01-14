using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMultiplayer : MonoBehaviour
{
    public int playerNumber;
    public bool debug;

    [HideInInspector]
    public GameObject control;

    public Rigidbody body;
    public Animator animator;
    public float thrust;
    public float turnSpeed;
    public float maxTurnSpeed = 150;

    private bool qPress;
    private bool wPress;
    private bool ePress;
    private bool ctrlA;
    private Vector3 Angle;
    
    Playerscript ps;

    public List<string> basketList = new List<string>();

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

        //Debug.Log("Player number: " + playerNumber + "'s x axis: " + Input.GetAxisRaw("joy" + playerNumber + "x"));
        float playerJoyX = Input.GetAxis("joy" + playerNumber + "x");
        //Check if x axis of the left joystick of this player isn't zero then calculate Angle by multiplying the turnSpeed into the input
        if (playerJoyX != 0)
        {
            Angle.y += (playerJoyX * turnSpeed) * (Time.deltaTime * 2);

            //Angle.y = Mathf.Clamp(Angle.y, -maxTurnSpeed, maxTurnSpeed);

            if (debug) Debug.Log(Angle.y);
        }
        else
        {
            //Angle = new Vector3(0, 0, 0);
            Angle = Vector3.Lerp(Angle, Vector3.zero, turnSpeed * Time.deltaTime);
        }

        animator.SetFloat("TurnValue", playerJoyX * turnSpeed);
        
        // body.AddForceAtPosition(transform.rotation.y * turnSpeed,body.gameObject.transform.position);

        // body.AddForce(transform.forward * Input.GetAxis("Thrust")); 
    }
    void FixedUpdate()
    {
        Vector3 force = transform.forward * thrust;
        bool moveForward = ctrlA || wPress;
        Vector3 rotationAngle = Angle * Time.deltaTime;
        
        if (moveForward) { body.AddForce(force); }
        animator.SetFloat("ForwardSpeed", body.velocity.magnitude);
        //if(debug)
            //Debug.Log(playerNumber + ": " + body.velocity.z);

        Quaternion deltaRotation = Quaternion.Euler(rotationAngle);
        
        body.MoveRotation(body.rotation * deltaRotation);
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
                        basketList.Add(item);
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
