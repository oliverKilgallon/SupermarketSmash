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
    private Vector3 lastTrolleyPos;
    private Vector3 currTrolleyPos;

    public float RotSpeed = 5;

    private void Start()
    {
        lastWheelPositions = new Vector3[wheelTransforms.Length];
        smoothedPositions = new Vector3[wheelTransforms.Length];
        wheelVelocities = new Vector3[wheelTransforms.Length];

        lastTrolleyPos = trolleyRigidbody.transform.position;
        currTrolleyPos = trolleyRigidbody.transform.position;

        for (int i = 0; i < wheelTransforms.Length; i++)
        {
            lastWheelPositions[i] = wheelTransforms[i].position;
        }
    }

    void Update()
    {
        currTrolleyPos = trolleyRigidbody.transform.position;

        for(int i = 0; i < wheelTransforms.Length; i++)
        {
            if (trolleyRigidbody.velocity.magnitude > 0)
            {
                //smoothedPositions[i] = Vector3.SmoothDamp(wheelTransforms[i].position, lastWheelPositions[i], ref wheelVelocities[i], 0.3f);
                float wheelAngle = Vector3.Angle(wheelTransforms[i].rotation.eulerAngles, trolleyRigidbody.rotation.eulerAngles);
                Vector3 newRotation = new Vector3(0, wheelAngle + wheelTransforms[i].rotation.eulerAngles.y, 0);
                //Debug.Log(wheelAngle);
                Vector3.RotateTowards(wheelTransforms[i].rotation.eulerAngles, newRotation, 50.0f, 10.0f);
                //wheelTransforms[i].LookAt(lastTrolleyPos, Vector3.up);

                //Quaternion wheelRot = new Quaternion(wheelTransforms[i].localRotation.x, wheelAngle, wheelTransforms[i].localRotation.z, wheelTransforms[i].localRotation.w);
                //Quaternion.RotateTowards(wheelTransforms[i].rotation, wheelRot, RotSpeed * Time.deltaTime);

                //wheelTransforms[i].LookAt(lastWheelPositions[i], Vector3.up);

                //wheelTransforms[i].Rotate(new Vector3(0, 180.0f, 0));
            }
            lastWheelPositions[i] = wheelTransforms[i].position;
            lastTrolleyPos = currTrolleyPos;
        }
    }
}