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

    public float thrust = 200;
    public float turnSpeed;
    public float maxAngDrag = 1.2f;
    public float minAngDrag = 0.1f;
    public bool useDragCurve = false;
    public AnimationCurve angularDragCurve;
    //Attempting standard torque values
    private float torque;
    private float evaluatedAngDrag;

    private bool qPress;
    private bool wPress;
    private bool ePress;
    private bool ctrlA;
    private Vector3 Angle;
    private Vector3 angularVel = Vector3.zero;
    
    Playerscript ps;

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
        
        float playerJoyX = Input.GetAxis("joy" + playerNumber + "x");

        torque = Input.GetAxis("Horizontal") * turnSpeed;

        //AccelAmount = playerJoyX * turnSpeed;
    }

    void FixedUpdate()
    {
        Vector3 force = transform.forward * thrust;
        bool moveForward = ctrlA || wPress;
        
        if (moveForward) { body.AddForce(force); }
        
        float scaledValue = (body.angularVelocity.magnitude) / body.maxAngularVelocity;
        
        evaluatedAngDrag = angularDragCurve.Evaluate(scaledValue);

        if (useDragCurve) { body.angularDrag = (evaluatedAngDrag * maxAngDrag) + minAngDrag; }

        body.AddTorque(transform.up * torque);

        animator.SetFloat("ForwardSpeed", body.velocity.magnitude);
        animator.SetFloat("TurnValue", torque);
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
        }
    }
    private void OnTriggerEnter(Collider col)
    {
       
        if (col.gameObject.tag == "jam")
        {
           
            body.velocity = new Vector3(body.velocity.x/speedStop,body.velocity.y/speedStop,body.velocity.z/speedStop);
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
    
}
