using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDuration : MonoBehaviour
{
    public float duration;
    bool off = true;
    bool off2 = true;
    public GameObject player;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("here");

        if (other.gameObject.tag == "Trail")
        {
            Debug.Log("here");
            player = other.gameObject;
           // StartCoroutine(trail(duration, other.gameObject));
        }
    }
    private void Start()
    {
        Debug.Log("i'm attaced to"+this.gameObject);
    }

    /*IEnumerator trail(float duration, GameObject trail)
    {
        off = false;
        /* foreach(TrailRenderer tr in trail.GetComponentsInChildren<TrailRenderer>())
        {   
            tr.emitting = true;
            tr.startColor = GetComponent<Renderer>().material.color;
        }
        
        yield return new WaitForSeconds(duration);
        foreach (TrailRenderer tr in trail.GetComponentsInChildren<TrailRenderer>())
        {
            tr.emitting = false;
            tr.endColor = GetComponent<Renderer>().material.color;
            tr.AddPosition(trail.transform.position);
        }*
        yield return new WaitForSeconds(duration);
    }
    */
    public void stopTrail(GameObject trail)
    {
        foreach (TrailRenderer tr in trail.GetComponentsInChildren<TrailRenderer>())
        {
            tr.emitting = false;
            tr.endColor = GetComponent<Renderer>().material.color;
            //tr.AddPosition(trail.transform.position);
        }
    }
}
