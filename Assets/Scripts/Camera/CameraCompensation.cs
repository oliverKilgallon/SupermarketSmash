using UnityEngine;

public class CameraCompensation : MonoBehaviour
{
    //Object to compensate
    public Transform cameraObj;

    public float cameraRayDistance = 5.0f;

    /**Fire raycast behind tracked object, if hit is less than wanted distance,
     * translate object along object's forward vector until this is no longer true
     */ 
    void FixedUpdate()
    {
        if (Physics.Raycast(cameraObj.position, -1 * cameraObj.forward, out RaycastHit hit, cameraRayDistance))
        {
            if (hit.distance < cameraRayDistance)
            {
                float compensationDist = cameraRayDistance - hit.distance;
                cameraObj.Translate(transform.forward * compensationDist * Time.fixedDeltaTime, Space.Self);
            }
        }
    }
}
