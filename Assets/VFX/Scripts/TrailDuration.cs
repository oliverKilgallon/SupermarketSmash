using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDuration : MonoBehaviour
{
    public float duration;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("here");

        if (other.gameObject.tag == "Trail")
        {
            StartCoroutine(trail(duration, other.gameObject));
        }
    }

    IEnumerator trail(float duration, GameObject trail)
    {
        foreach(TrailRenderer tr in trail.GetComponentsInChildren<TrailRenderer>())
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
        }
    }
}
