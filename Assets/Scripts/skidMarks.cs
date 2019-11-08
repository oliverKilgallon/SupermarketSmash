using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skidMarks : MonoBehaviour
{
    public GameObject skid;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("hit");

        if (collision.transform.gameObject.tag == "floor")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Instantiate(skid, new Vector3(contact.point.x, contact.point.y+.05f, contact.point.z), collision.transform.rotation);
            }
        }
    }
}
