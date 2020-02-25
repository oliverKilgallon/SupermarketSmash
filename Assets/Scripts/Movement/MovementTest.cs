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
    public float baseBrakeMagnitude = 6.0f;
    public float baseTurnMagnitude = 1.0f;
    public float turnMassNormaliser = 0.2f;
    public float angularVelocity;
    public float angularVelocityDecayRate = 3.0f;
    public float angularVelocityMaxMagnitude = 3.0f;
    public float totalMass = 250.0f;
    public float deadZone = 0.4f;
    public float angularCorrectValue = 2.0f;
    public float perlinFreq = 1;
    public float perlinScale = 0.1f;
    public float perlinThreshold = 0.75f;

    public float smoothValue = 0.15f;
    float smoothVelocity;
    float velocityDir;
    float perlinXVal;

    private bool wPress;
    private bool ctrlA;
    private bool decelerate;
    private float playerJoyX;

    public float soundCooldown = 0.5f;
    private float timeSoundPlayed;

    Playerscript ps;

    //Variables related to powerups/hazards/items
    public bool jammy;
    public float speedSticky;
    public float speedStop;
    public List<string> basketList = new List<string>();
    public GameObject impactAnim;
    public GameObject animRotation;
    public AnimationCurve tiltCurve;

    void Start()
    {
        ps = GetComponent<Playerscript>();
        body = GetComponent<Rigidbody>();
        jammy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("joy" + playerNumber + "Acc") != 0) { ctrlA = true; }
        if (Input.GetAxis("joy" + playerNumber + "Acc") == 0) { ctrlA = false; }
        if (Input.GetAxis("joy" + playerNumber + "Dec") != 0) { decelerate = true; }
        if (Input.GetAxis("joy" + playerNumber + "Dec") == 0) { decelerate = false; }
        if (Input.GetKeyDown("w")) { wPress = true; }
        if (Input.GetKeyUp("w")) { wPress = false; }

        playerJoyX = Input.GetAxis("joy" + playerNumber + "x");
    }

    void FixedUpdate()
    {
        Vector3 thrustForce = transform.forward * baseMoveMagnitude;
        Vector3 brakeForce = transform.forward * baseMoveMagnitude;
        Vector2 bodyVel = new Vector2(body.velocity.x, body.velocity.z);
        bool forward = ctrlA || wPress;
        
        if (ctrlA || wPress)
        {
            body.AddForce(thrustForce, ForceMode.Acceleration);
            
        }

        if (decelerate)
        {
            body.AddForce(body.velocity.normalized * -baseBrakeMagnitude, ForceMode.Acceleration);
        }

        if (playerJoyX > deadZone && Mathf.Abs(angularVelocity) < angularVelocityMaxMagnitude)
        {
            angularVelocity += (baseTurnMagnitude / (totalMass * turnMassNormaliser)) * Mathf.Abs(playerJoyX);

            if (angularVelocity < 0)
            {
                angularVelocity *= angularCorrectValue;
            }
        }
        else if (playerJoyX < -deadZone && Mathf.Abs(angularVelocity) < angularVelocityMaxMagnitude)
        {
            angularVelocity -= (baseTurnMagnitude / (totalMass * turnMassNormaliser)) * Mathf.Abs(playerJoyX);

            if (angularVelocity > 0)
            {
                angularVelocity *= angularCorrectValue;
            }
        }

        angularVelocity *= angularVelocityDecayRate;// Mathf.Lerp(angularVelocity, 0, angularVelocityDecayRate / (totalMass * turnMassNormaliser));

        bodyVel = new Vector2(body.velocity.x, body.velocity.z);

        body.angularVelocity = new Vector3(0, angularVelocity, 0);

        velocityDir = Mathf.SmoothDamp(velocityDir, Vector3.Dot(body.velocity, body.transform.forward), ref smoothVelocity, smoothValue);
        animator.SetFloat("ForwardSpeed", velocityDir);
        
        

        float value = tiltCurve.Evaluate(Mathf.Abs(body.angularVelocity.y));

        value *= Mathf.Sign(body.angularVelocity.y);

        perlinXVal += perlinFreq * Time.fixedDeltaTime;
        float perlin = Mathf.PerlinNoise(perlinXVal, 0);
        
        if (Mathf.Abs(value) > perlinThreshold)
        {
            float perlinStrength = Mathf.InverseLerp(perlinThreshold, 1, Mathf.Abs(value));
            perlin *= perlinScale * perlinStrength;
            value -= perlin*Mathf.Sign(value);
        }
        
        //Turn value should be equal to how fast we are rotating per physics frame
        trolleyAnimator.SetFloat("AngularVel", value);
        animator.SetFloat("TurnValue", value);
    }

    private bool IsSameSign(float num1, float num2)
    {
        return num1 >= 0 && num2 >= 0 || num1 < 0 && num2 < 0;
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag != ("floor") && col.gameObject.tag != ("item") && col.gameObject.tag != ("box"))
        {
            //float rot = Vector3.Angle(animRotation.transform.position,col.GetContact(0).point);
            var rot = Quaternion.LookRotation(col.GetContact(0).normal);
            Instantiate(impactAnim, col.GetContact(0).point, rot);

            //Play a random trolley bash sound out of the amount we have
            if (Time.time > (timeSoundPlayed + soundCooldown))
            {
                if (col.relativeVelocity.magnitude > 6.0f)
                {
                    SoundManager.instance.PlaySound("Trolley Bash " + Random.Range(1, 3), false);
                    timeSoundPlayed = Time.time;
                }
            }
        }
        if (col.gameObject.tag == "item")
        {
            int i = 0;
            foreach (string item in ps.localItems)
            {
                if (ps.localItems[i] != "" && ps.localItems[i] != null)
                {
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


