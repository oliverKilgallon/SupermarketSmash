using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impulse : MonoBehaviour
{

    public GameObject[] cereal;
    public GameObject explosionPoint;
    public float explosionForce;

    private IEnumerator freeze;
    public float waitTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0), ForceMode.Acceleration);



    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.tag != "floor")
        {
        //GetComponent<Rigidbody>().isKinematic = true;
        foreach (GameObject cb in cereal)
        {
            Debug.Log("BOOM");
            cb.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(explosionForce-30, explosionForce+30), explosionPoint.transform.position, 200);
        }
        freeze = freezeObjects(waitTime);
        StartCoroutine(freeze);
        }
    }

    IEnumerator freezeObjects(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        foreach (GameObject cb in cereal)
        {
            cb.GetComponent<Rigidbody>().isKinematic = true;
        }
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
