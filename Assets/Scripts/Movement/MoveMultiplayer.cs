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

    //Variables related to turn movement
    public float thrust = 200;
    public float turnSpeed;
    public float reverseTorqueScale = 1.0f;
    public bool useDragCurve = false;
    public AnimationCurve angularDragCurve;
    private float turnInput;

    private bool qPress;
    private bool wPress;
    private bool ePress;
    private bool ctrlA;
    private float playerJoyX;
    
    Playerscript ps;

    //Variables related to powerups/hazards/items
    public bool jammy;
    public float speedSticky;
    public float speedStop;
    public List<string> basketList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<Playerscript>();
        body.interpolation = RigidbodyInterpolation.Interpolate;
        jammy = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("joy"+playerNumber+"Acc")) { ctrlA = true; }if (Input.GetKeyUp("joy"+playerNumber+"Acc")) { ctrlA = false; }
        if (Input.GetAxis("joy" + playerNumber + "Acc")!=0) { ctrlA = true; }
        if (Input.GetAxis("joy" + playerNumber + "Acc")==0) { ctrlA = false; }
        if (Input.GetKeyDown("q")) { qPress = true; }if (Input.GetKeyUp("q")) { qPress = false; }
        if (Input.GetKeyDown("w")) { wPress = true; }if (Input.GetKeyUp("w")) { wPress = false; }
        if (Input.GetKeyDown("e")) { ePress = true; }if (Input.GetKeyUp("e")) { ePress = false; }
        
        playerJoyX = Input.GetAxis("joy" + playerNumber + "x");

        turnInput = Input.GetAxis("Horizontal");

        //AccelAmount = playerJoyX * turnSpeed;
    }

    void FixedUpdate()
    {
        Vector3 force = transform.forward * thrust;
        bool moveForward = ctrlA || wPress;
        
        if (moveForward) { body.AddForce(force); }
        
        float angVelPercent = body.angularVelocity.magnitude / body.maxAngularVelocity;
        
        //Scale torque according to how fast we are currently rotating
        float torqueScale = 1 - angularDragCurve.Evaluate(angVelPercent);

        //evaluatedAngDrag = angularDragCurve.Evaluate(angVelPercent);
        //if (useDragCurve) { body.angularDrag = (evaluatedAngDrag * maxAngDrag) + minAngDrag; }

        //If input direction is the same as angular velocity, apply decreasing amounts of torque
        //relative to how fast we are rotating
        //Else apply full torque in other direction
        if (useDragCurve && (IsSameSign(playerJoyX, body.angularVelocity.y)))
        {
            body.AddTorque(transform.up * ((turnSpeed * torqueScale) * playerJoyX));
        }
        else if (useDragCurve)
        {
            body.AddTorque(transform.up * (turnSpeed * playerJoyX * reverseTorqueScale));
        }
        else
        {
            body.AddTorque(transform.up * turnSpeed * playerJoyX);
        }

        //Forward speed is equal to current speed, scaled to be between 0 and 1
        animator.SetFloat("ForwardSpeed", body.velocity.normalized.z);

        //Turn value should be equal to how fast we are rotating per physics frame
        animator.SetFloat("TurnValue", playerJoyX);
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
                    //Debug.Log(item + " " + col.gameObject.GetComponent<ItemScript>().product);
                    if ((item == col.gameObject.GetComponent<ItemScript>().product)&&(ps.heldItem == ""))
                    {
                        
                       // basketList.Add(item);
                        ps.listText[ps.localItems.IndexOf(item)].text = "";
                        ps.localItems[ps.localItems.IndexOf(item)] = "";
                        ps.currentHeld.Add(item);
                        ps.heldItem = item;
                       
                        Destroy(col.gameObject);
                        break;

                    }

                }
                i++;
            }
        }
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "checkout") { ps.heldItem = ""; Debug.Log("Checkout"); }
        if (col.gameObject.tag == "jam")
        {
           
           // body.velocity = new Vector3(body.velocity.x/speedStop,body.velocity.y/speedStop,body.velocity.z/speedStop);
            jammy = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "jam")
        {
            jammy = false;
        }
    }

    private bool IsSameSign(float num1, float num2)
    {
        return num1 >= 0 && num2 >= 0 || num1 < 0 && num2 < 0;
    }
    
}
