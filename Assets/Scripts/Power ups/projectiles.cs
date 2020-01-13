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
    
    public int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<MoveMultiplayer>().playerNumber;
        arc = new Vector3[16];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("joy" + playerNumber + "Throw"))
        {
           
            projectileMode = true;
            Pointer.GetComponent<MeshRenderer>().enabled = true;
            Pointer.transform.RotateAround(PointerBase.transform.position,Vector3.up, -adjust);
            adjust = 0;

        }
        if (Input.GetButtonUp("joy" + playerNumber + "Throw"))
        {
                    
                for(int i = 1;i<17 ; i++)
                {
                    arc[i - 1] = thrownPoints[i].transform.position;
                    
   
                }
            GameObject thisThrow;
            thisThrow = (GameObject)Instantiate(throwable, thrownPoints[currentCurvePoint].transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            
            
                thisThrow.GetComponent<Throw>().localArc =  arc;
                
            
            
            
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
