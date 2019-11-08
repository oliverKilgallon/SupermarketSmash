using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyWheelRotation : MonoBehaviour
{
    public Transform[] wheelTransforms;
    public Rigidbody trolleyRigidbody;
    
    private Vector3[] lastWheelPositions;
    private Vector3[] smoothedPositions;
    private Vector3[] wheelVelocities;

    private void Start()
    {
        lastWheelPositions = new Vector3[wheelTransforms.Length];
        smoothedPositions = new Vector3[wheelTransforms.Length];
        wheelVelocities = new Vector3[wheelTransforms.Length];

        for (int i = 0; i < wheelTransforms.Length; i++)
        {
            lastWheelPositions[i] = wheelTransforms[i].position;
        }
    }

    void FixedUpdate()
    {
        for(int i = 0; i < wheelTransforms.Length; i++)
        {
            if (trolleyRigidbody.velocity.magnitude > 0)
            {
                smoothedPositions[i] = Vector3.SmoothDamp(wheelTransforms[i].position, lastWheelPositions[i], ref wheelVelocities[i], 0.3f);
                
                wheelTransforms[i].LookAt(smoothedPositions[i], Vector3.up);
            }

            lastWheelPositions[i] = smoothedPositions[i];
        }
    }
}
