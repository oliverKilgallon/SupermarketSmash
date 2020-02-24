using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public int playerNumber;

    [HideInInspector]
    public GameObject control;

    public Rigidbody body;
    public Animator animator;

    //Variables related to turn movement
    public float baseMoveMagnitude = 12.0f;
    public float baseTurnMagnitude = 1.0f;
    public float turnMassNormaliser = 0.2f;
    public float angularVelocity;
    public float angularVelocityDecayRate = 3.0f;
    public float angularVelocityMaxMagnitude = 3.0f;
    public float totalMass = 250.0f;
    public float deadZone = 0.4f;
    public float angularCorrectValue = 2.0f;
    private float rotationDelta;
    
    private bool wPress;
    private bool ctrlA;
    private bool decelerate;
    private float playerJoyX;

    Playerscript ps;

    //Variables related to powerups/hazards/items
    public bool jammy;
    public float speedSticky;
    public float speedStop;
    public List<string> basketList = new List<string>();

    void Start()
    {
        ps = GetComponent<Playerscript>();
        body = GetComponent<Rigidbody>();
        jammy = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("joy"+playerNumber+"Acc")) { ctrlA = true; }if (Input.GetKeyUp("joy"+playerNumber+"Acc")) { ctrlA = false; }
        if (Input.GetAxis("joy" + playerNumber + "Acc") != 0) { ctrlA = true; }
        if (Input.GetAxis("joy" + playerNumber + "Acc") == 0) { ctrlA = false; }
        if (Input.GetAxis("joy" + playerNumber + "Dec") != 0) { decelerate = true; }
        if (Input.GetAxis("joy" + playerNumber + "Dec") == 0) { decelerate = false; }
        if (Input.GetKeyDown("w")) { wPress = true; }
        if (Input.GetKeyUp("w")) { wPress = false; }

        playerJoyX = Input.GetAxis("joy" + playerNumber + "x");

        gameObject.transform.Rotate(Vector3.up, angularVelocity);
    }

    void FixedUpdate()
    {
        Vector3 thrustForce = transform.forward * baseMoveMagnitude;
        Vector3 brakeForce = transform.forward * baseMoveMagnitude;
        
        if (ctrlA || wPress)
        {
            body.AddForce(transform.forward * baseMoveMagnitude, ForceMode.Acceleration);
            if (!SoundManager.instance.IsSoundPlaying("Trolley Movement Rustle")) SoundManager.instance.PlaySound("Trolley Movement Rustle", false);
        }

        if (decelerate)
        {
            body.AddForce(body.velocity.normalized * -baseMoveMagnitude, ForceMode.Acceleration);
        }

        if (playerJoyX > deadZone && Mathf.Abs(angularVelocity) < angularVelocityMaxMagnitude)
        {
            if (angularVelocity < 0)
            {
                angularVelocity += (baseTurnMagnitude / (totalMass * turnMassNormaliser)) * angularCorrectValue * Mathf.Abs(playerJoyX);
            }
            else
            {
                angularVelocity += (baseTurnMagnitude / (totalMass * turnMassNormaliser)) * Mathf.Abs(playerJoyX);
            }
        }
        else if (playerJoyX < -deadZone && Mathf.Abs(angularVelocity) < angularVelocityMaxMagnitude)
        {
            if (angularVelocity > 0)
            {
                angularVelocity -= (baseTurnMagnitude / (totalMass * turnMassNormaliser)) * angularCorrectValue * Mathf.Abs(playerJoyX);
            }
            else
            {
                angularVelocity -= (baseTurnMagnitude / (totalMass * turnMassNormaliser)) * Mathf.Abs(playerJoyX);
            }
        }
        else
        {
            angularVelocity = Mathf.Lerp(angularVelocity, 0, angularVelocityDecayRate / (totalMass * turnMassNormaliser));
        }
        
        //Forward speed is equal to current speed, scaled to be between 0 and 1
        animator.SetFloat("ForwardSpeed", body.velocity.normalized.z);

        //Turn value should be equal to how fast we are rotating per physics frame
        animator.SetFloat("TurnValue", playerJoyX);
    }



    private void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("item"))
        {
            return;
        }

        foreach (string item in ps.localItems)
        {
            if (!string.IsNullOrEmpty(item))
            {
                //Debug.Log(item + " " + col.gameObject.GetComponent<ItemScript>().product);
                if ((item == col.gameObject.GetComponent<ItemScript>().product) && (ps.heldItem == ""))
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
