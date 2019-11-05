using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetPos = new Vector3(0, 4, -5);
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private float yVelocity = 0.0f;
    private float distance = 5.0f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smoothTime);

        targetPosition = target.TransformPoint(offsetPos);

        targetPosition += Quaternion.Euler(0, yAngle, 0) * new Vector3(0, 0, -distance);

        transform.LookAt(target);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
