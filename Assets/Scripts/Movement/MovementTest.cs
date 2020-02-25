using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public int playerNumber;

    [HideInInspector]
    public GameObject control;

    public Rigidbody body;
    public Animator animator;
    public Animator trolleyAnimator;

    //Variables related to turn movement
    public float baseMoveMagnitude = 12.0f;
    public float baseJamMagnitude = 9.0f;
    public float baseBrakeMagnitude = 6.0f;
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
    public GameObject impactAnim;
    public GameObject animRotation;
    public GameObject[] wheelSmoke;

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
        if (Input.GetAxis("joy" + playerNumber + "Acc") != 0) { ctrlA = true;foreach (GameObject Sm in wheelSmoke) { if (!Sm.GetComponent<ParticleSystem>().isPlaying) { Sm.GetComponent<ParticleSystem>().Play(); } } }
        if (Input.GetAxis("joy" + playerNumber + "Acc") == 0) { ctrlA = false; foreach(GameObject Sm in wheelSmoke) { Sm.GetComponent<ParticleSystem>().Stop(); } }

       // if (Input.GetAxis("joy" + playerNumber + "Acc") != 0) { ctrlA = true; foreach (GameObject Sm in wheelSmoke) { Sm.SetActive(true); } }
        //if (Input.GetAxis("joy" + playerNumber + "Acc") == 0) { ctrlA = false; foreach (GameObject Sm in wheelSmoke) { Sm.SetActive(false); } }
        if (Input.GetAxis("joy" + playerNumber + "Dec") != 0) { decelerate = true; }
        if (Input.GetAxis("joy" + playerNumber + "Dec") == 0) { decelerate = false; }
        if (Input.GetKeyDown("w")) { wPress = true; }
        if (Input.GetKeyUp("w")) { wPress = false; }

        playerJoyX = Input.GetAxis("joy" + playerNumber + "x");

        
    }

    void FixedUpdate()
    {
        if (!jammy)
        {
            Vector3 thrustForce = transform.forward * baseMoveMagnitude;
            if (ctrlA || wPress)
            {
                body.AddForce(thrustForce, ForceMode.Acceleration);
            }
        }
        else
        {
            Vector3 thrustForce = transform.forward * baseJamMagnitude;
            if (ctrlA || wPress)
            {
                body.AddForce(thrustForce, ForceMode.Acceleration);
            }
        }
        
        Vector3 brakeForce = transform.forward * baseMoveMagnitude;
        bool forward = ctrlA || wPress;
        
        

        if (decelerate)
        {
            body.AddForce(body.velocity.normalized * -baseBrakeMagnitude, ForceMode.Acceleration);
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

        //gameObject.transform.Rotate(Vector3.up, angularVelocity);

        body.angularVelocity = new Vector3(0, angularVelocity, 0);

        //Forward speed is equal to current speed, scaled to be between 0 and 1
        animator.SetFloat("ForwardSpeed", body.velocity.z);

        trolleyAnimator.SetFloat("AngularVel", body.angularVelocity.y);

        //Turn value should be equal to how fast we are rotating per physics frame
        animator.SetFloat("TurnValue", body.angularVelocity.y);
    }

    /*private void OnCollisionEnter(Collision col)
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
    }*/

    private bool IsSameSign(float num1, float num2)
    {
        return num1 >= 0 && num2 >= 0 || num1 < 0 && num2 < 0;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != ("floor")&& col.gameObject.tag != ("item")&& col.gameObject.tag != ("box"))
        {
            //float rot = Vector3.Angle(animRotation.transform.position,col.GetContact(0).point);
            Debug.DrawRay(col.GetContact(0).point, col.GetContact(0).normal, Color.black, 5, true);
            var rot = Quaternion.LookRotation(col.GetContact(0).normal);
            Instantiate(impactAnim, col.GetContact(0).point, rot);
        }
        if (col.gameObject.tag == "item")
        {
            int i = 0;
            foreach (string item in ps.localItems)
            {
                if (ps.localItems[i] != "" && ps.localItems[i] != null)
                {
                    //Debug.Log(item + " " + col.gameObject.GetComponent<ItemScript>().product);
                    /*if ((item == col.gameObject.GetComponent<ItemScript>().product)&&(ps.heldItem == ""))
                    {
                        
                       // basketList.Add(item);
                        ps.listText[ps.localItems.IndexOf(item)].text = "";
                        ps.localItems[ps.localItems.IndexOf(item)] = "";
                        ps.currentHeld.Add(item);
                        ps.heldItem = item;
                       
                        Destroy(col.gameObject);
                        break;

                    }*/
                    if ((item == col.gameObject.GetComponent<ItemScript>().product))
                    {

                        // basketList.Add(item);
                        ps.listText[ps.localItems.IndexOf(item)].text = "";
                        ps.localItems[ps.localItems.IndexOf(item)] = "";
                        ps.currentHeld.Add(item);
                        // ps.heldItem = item;

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
        //  if (col.gameObject.tag == "checkout") { ps.heldItem = ""; Debug.Log("Checkout"); }
        if (col.gameObject.tag == "checkout") { ps.currentHeld = new List<string>(); }
        if (col.gameObject.tag == "jam")
        {
            
            body.velocity = new Vector3(body.velocity.x / speedStop, body.velocity.y / speedStop, body.velocity.z / speedStop);
            // body.velocity = new Vector3( speedStop, speedStop, speedStop);
            jammy = true;
            col.GetComponentInParent<Throw>().jammyPlayer = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "jam")
        {
            jammy = false;
            StartCoroutine(Timer(1,col.GetComponent<Renderer>().material.color));
        }
    }
    IEnumerator Timer(float duration,Color jamColor)
    {
        Debug.Log("here");
        foreach (TrailRenderer tr in GetComponentsInChildren<TrailRenderer>())
        {
            tr.emitting = true;
             tr.startColor = jamColor;
             tr.AddPosition(transform.position);
        }

        yield return new WaitForSeconds(duration);

        Debug.Log("here + duration");
        foreach (TrailRenderer tr in GetComponentsInChildren<TrailRenderer>())
        {
            tr.emitting = false;
            //  tr.endColor = GetComponent<Renderer>().material.color;
            // tr.AddPosition(trail.transform.position);
        }
    }
   

}


