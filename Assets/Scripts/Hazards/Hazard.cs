using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float duration;
    public float spinForce;

    public void ApplyEffect(string effect, GameObject go)
    {
        switch (effect) { }
    }

    private IEnumerator Spin(GameObject go)
    {
        if (go.GetComponent<Rigidbody>()) { 
            for (float i = 0; i < duration; i += Time.fixedDeltaTime)
            {
                go.GetComponent<Rigidbody>().AddTorque(new Vector3(0, spinForce * Time.fixedDeltaTime, 0));
            }
        }
        else
        {
            for (float i = 0; i < duration; i += Time.deltaTime)
            {
                go.transform.RotateAround(go.transform.localPosition, go.transform.up, spinForce);
            }
        }

        yield return null;
    }

    private void RemoveFriction()
    {
        
    }
}
