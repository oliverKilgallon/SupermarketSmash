using UnityEngine;

public class CameraCompensation : MonoBehaviour
{
    //Object to compensate
    public Transform cameraObj;

    public float cameraRayDistance = 5.0f;
    public float leftRightRayDistance = 1.0f;

    private float compensationDist;

    /**Fire raycast behind tracked object, if hit is less than wanted distance,
     * translate object along object's forward vector until this is no longer true
     */ 
    void FixedUpdate()
    {
        //Backwards object tracking
        if (Physics.Raycast(cameraObj.position, -1 * cameraObj.forward, out RaycastHit hit, cameraRayDistance))
        {
            if (hit.distance < cameraRayDistance)
            {
                compensationDist = cameraRayDistance - hit.distance;
                cameraObj.Translate(transform.forward * compensationDist * Time.fixedDeltaTime, Space.Self);
            }
        }
        
        //Right side object tracking
        if (Physics.Raycast(cameraObj.position, cameraObj.right, out RaycastHit rightHit, leftRightRayDistance / 2.0f))
        {
            if ((rightHit.distance / 2.0f) < leftRightRayDistance)
            {
                compensationDist = leftRightRayDistance - rightHit.distance;
                cameraObj.Translate((transform.right) * compensationDist * Time.fixedDeltaTime, Space.Self);
            }
        }

        //Left side object tracking
        if (Physics.Raycast(cameraObj.position, -1 * cameraObj.right, out RaycastHit leftHit, cameraRayDistance / 2.0f))
        {
            if ((leftHit.distance / 2.0f) < cameraRayDistance)
            {
                compensationDist = cameraRayDistance - leftHit.distance;
                cameraObj.Translate(transform.right * compensationDist * Time.fixedDeltaTime, Space.Self);
            }
        }
        
    }
}
