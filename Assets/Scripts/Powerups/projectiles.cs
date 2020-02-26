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

    public Vector3 aimStart;
    public GameObject aimCursor;
    public float aimx;
    public float aimy;

    public float shiftx;
    public float shifty;
    public float spin360;

    public int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<MovementTest>().playerNumber;
        arc = new Vector3[16];
       // floorY = floor.gameObject.transform.position.y;
        currentlyUsable = true;
        timer = 5;
        timeLock = 0;
        currentWeapon = null;
        //aimStart = aimCursor.gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // spin360 = (transform.rotation.y / 360);// -(transform.rotation.y% 360);
        //aimx = Input.GetAxis("joy" + playerNumber + "aimx");
        // aimy = Input.GetAxis("joy" + playerNumber + "aimy");
        // Vector3 aim = new Vector3(Input.GetAxis("joy" + playerNumber + "aimx") * -shiftx, aimStart.y, Input.GetAxis("joy" + playerNumber + "aimy") * shifty);

        // Vector3 angles = new Vector3(0, transform.rotation.y, 0);
        //Vector3 dir = aim - transform.position;
        // dir = Quaternion.Euler() * dir;
        //aimCursor.gameObject.transform.localPosition = aim;
        // aimCursor.gameObject.transform.localPosition = Quaternion.Euler(angles) * (aim - transform.position) + transform.position;
        //aimCursor.gameObject.transform.position = transform.position + aim;
        // aimCursor.gameObject.transform.position = transform.position +dir;
        if (timer < ThrowCountdown + 1)
        {
            timer += Time.deltaTime;
        }
        if (currentWeapon != null)
        {
           

            /// if (Input.GetKeyDown(KeyCode.A)) { currentWeapon = "flour"; }
            // if (Input.GetKeyDown(KeyCode.S)) { currentWeapon = "jam"; }
            if (Input.GetButtonDown("joy" + playerNumber + "Throw") && timer > ThrowCountdown)
            {

                timeLock = timer;
                projectileMode = true;
                Pointer.GetComponent<MeshRenderer>().enabled = true;
                //   Pointer.transform.RotateAround(PointerBase.transform.position,Vector3.up, -adjust);
                adjust = 0;

            }


            if (Input.GetButtonUp("joy" + playerNumber + "Throw") && timeLock > ThrowCountdown)
            {
                timeLock = 0;
                timer = 0;
                for (int i = 1; i < 17; i++)
                {
                    arc[i - 1] = thrownPoints[i].transform.position;
                    //Debug.Log(i);

                }
                GameObject thisThrow;
                thisThrow = (GameObject)Instantiate(throwable, thrownPoints[currentCurvePoint].transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));



                // 
                if(currentWeapon == "jam") { thisThrow.GetComponent<Throw>().jamModel.SetActive(true); thisThrow.GetComponent<Throw>().flourModel.SetActive(false); }
                if(currentWeapon == "flour") { thisThrow.GetComponent<Throw>().flourModel.SetActive(true); thisThrow.GetComponent<Throw>().jamModel.SetActive(false); }
                thisThrow.GetComponent<Throw>().localArc = arc;
                //arc.CopyTo(thisThrow.GetComponent<Throw>().localArc, 0);
                thisThrow.GetComponent<Throw>().weapon = currentWeapon;
                thisThrow.GetComponent<Throw>().floorY = floorY;




               // GetComponentInParent<powerupSlot>().current = null;
                GetComponentInParent<powerupSlot>().slot.texture = null;

                projectileMode = false;
                Pointer.GetComponent<MeshRenderer>().enabled = false;
                currentWeapon = null;

                SoundManager.instance.PlaySound("Normal Throw " + Random.Range(1, 2), false);
                // Pointer.transform.rotation = pointerTransform;
            }
        }
       
     
        
    }
}
