using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetFollow;
    public Vector3 offsetPos = new Vector3(0, 2, -4);
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetFollowPosition;
    private float yRotVelocity = 0.0f;
    private float distance = 5.0f;

    // Update is called once per frame
    void LateUpdate()
    {

        //float yAngleRelativeToPlayer = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetFollow.eulerAngles.y, ref yRotVelocity, smoothTime);

        targetFollowPosition = targetFollow.position + (targetFollow.rotation * offsetPos);

        //targetFollowPosition += Quaternion.Euler(0, yAngleRelativeToPlayer, 0) * new Vector3(0, 0, -distance);

        transform.position = Vector3.SmoothDamp(transform.position, targetFollowPosition, ref velocity, smoothTime);

        
        transform.LookAt(targetFollow, targetFollow.up);
    }
}
