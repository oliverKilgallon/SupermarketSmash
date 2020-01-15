using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectiles : MonoBehaviour 
{
    public bool projectileMode;
    public GameObject Pointer;
    public GameObject PointerBase;
    public float pointSpeed;
    public float adjust;
    public float spinlock;
    public GameObject[] thrownPoints;
    public Vector3[] arc;
    public int currentCurvePoint = 0;
    public GameObject throwable;
    public string currentWeapon;
    public float timer;
    public float ThrowCountdown;

    public GameObject floor;
    public float floorY;
    public bool currentlyUsable;
    public float timeLock;

    public int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<MoveMultiplayer>().playerNumber;
        arc = new Vector3[16];
        floorY = floor.gameObject.transform.position.y;
        currentlyUsable = true;
        timer = 5;
        timeLock = 0;

    }

    // Update is called once per frame
    void Update()
    {   if (timer < ThrowCountdown+1)
        {
            timer += Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.A)) { currentWeapon = "flour"; }
        if (Input.GetKeyDown(KeyCode.S)) { currentWeapon = "jam"; }
        if (Input.GetButtonDown("joy" + playerNumber + "Throw")&&timer>ThrowCountdown)
        {

            timeLock = timer;
            projectileMode = true;
            Pointer.GetComponent<MeshRenderer>().enabled = true;
            Pointer.transform.RotateAround(PointerBase.transform.position,Vector3.up, -adjust);
            adjust = 0;

        }
       

        if (Input.GetButtonUp("joy" + playerNumber + "Throw")&&timeLock>ThrowCountdown)
        {
            timeLock = 0;
            timer = 0;
            for (int i = 1;i<17 ; i++)
                {
                    arc[i - 1] = thrownPoints[i].transform.position;
                //Debug.Log(i);
   
                }
            GameObject thisThrow;
            thisThrow = (GameObject)Instantiate(throwable, thrownPoints[currentCurvePoint].transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            
            
            thisThrow.GetComponent<Throw>().localArc =  arc;
            //arc.CopyTo(thisThrow.GetComponent<Throw>().localArc, 0);
            thisThrow.GetComponent<Throw>().weapon = currentWeapon;
            thisThrow.GetComponent<Throw>().floorY = floorY;






            projectileMode = false;
            Pointer.GetComponent<MeshRenderer>().enabled = false;
            
            // Pointer.transform.rotation = pointerTransform;
        }
        if (projectileMode)
        {
           if ((adjust < spinlock)&&(adjust > (spinlock*-1)))
           {
                Pointer.transform.RotateAround(PointerBase.transform.position, Vector3.up, Input.GetAxis("joy" + playerNumber + "x") * pointSpeed);
                adjust += Input.GetAxis("joy" + playerNumber + "x") * pointSpeed;
           }

           if ((adjust >= spinlock)&&(Input.GetAxis("joy" + playerNumber + "x")<0))
           {
                Pointer.transform.RotateAround(PointerBase.transform.position, Vector3.up, Input.GetAxis("joy" + playerNumber + "x") * pointSpeed);
                adjust += Input.GetAxis("joy" + playerNumber + "x") * pointSpeed;
            }
            if ((adjust <= (spinlock*-1)) && (Input.GetAxis("joy" + playerNumber + "x") > 0))
            {
                Pointer.transform.RotateAround(PointerBase.transform.position, Vector3.up, Input.GetAxis("joy" + playerNumber + "x") * pointSpeed);
                adjust += Input.GetAxis("joy" + playerNumber + "x") * pointSpeed;
            }

        }
     
        
    }
}
