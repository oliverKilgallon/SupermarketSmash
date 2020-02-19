using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetFollowTransform;
    public Rigidbody rb;
    public Vector3 cameraOffsetPos = new Vector3(0, 2, -4);
    public float smoothDampingTime = 0.2f;
    public float rotSpeed = 1.2f;

    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 cameraRotVelocity = Vector3.zero;
    private Vector3 targetFollowPosition;

    // Update is called once per frame
    void LateUpdate()
    {
        targetFollowPosition = 
            targetFollowTransform.position + (rb.rotation * cameraOffsetPos);
        
        transform.position = 
            Vector3.SmoothDamp(
                transform.position, 
                targetFollowPosition, 
                ref cameraVelocity, 
                smoothDampingTime);

        Quaternion targetRot = Quaternion.Euler(transform.eulerAngles.x, rb.rotation.eulerAngles.y, transform.eulerAngles.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);

        //transform.LookAt(targetFollowTransform, targetFollowTransform.up);
    }
}
