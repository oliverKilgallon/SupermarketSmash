using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetFollowTransform;
    public Vector3 cameraOffsetPos = new Vector3(0, 2, -4);
    public float smoothDampingTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetFollowPosition;

    // Update is called once per frame
    void LateUpdate()
    {
        targetFollowPosition = 
            targetFollowTransform.position + (targetFollowTransform.rotation * cameraOffsetPos);
        
        transform.position = 
            Vector3.SmoothDamp(
                transform.position, 
                targetFollowPosition, 
                ref velocity, 
                smoothDampingTime);

        transform.LookAt(targetFollowTransform, targetFollowTransform.up);
    }
}
