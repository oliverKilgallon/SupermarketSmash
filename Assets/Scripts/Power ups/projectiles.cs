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
    
    int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<MoveMultiplayer>().playerNumber;
        
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
            projectileMode = false;
            Pointer.GetComponent<MeshRenderer>().enabled = false;
           // Pointer.transform.rotation = pointerTransform;
        }
        if (projectileMode)
        {
           
            Pointer.transform.RotateAround(PointerBase.transform.position, Vector3.up, Input.GetAxis("joy" + playerNumber + "x") * pointSpeed);
            adjust += Input.GetAxis("joy" + playerNumber + "x") * pointSpeed;
        }
     
        
    }
}
