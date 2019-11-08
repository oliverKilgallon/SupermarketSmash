using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifespan : MonoBehaviour
{
    public float lifeSpan; 
    void FixedUpdate()
    {
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
        lifeSpan -= Time.deltaTime;
    }
}
