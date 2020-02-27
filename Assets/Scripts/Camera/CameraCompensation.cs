using UnityEngine;

public class CameraCompensation : MonoBehaviour
{
    public Transform cameraObj;
    public float cameraRayDistance = 3.0f;
    Vector3 originalPos;
    float compensationDist = 0f;

    private void Start()
    {
        originalPos = cameraObj.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(cameraObj.position, -1 * cameraObj.forward, out RaycastHit hit, cameraRayDistance))
        {
            if (hit.distance < cameraRayDistance)
            {
                compensationDist = cameraRayDistance - hit.distance;
                cameraObj.Translate(transform.forward * compensationDist * Time.fixedDeltaTime, Space.Self);
            }
        }
        else if(cameraObj.localPosition != originalPos)
        {
            cameraObj.Translate((originalPos - cameraObj.localPosition) * Time.fixedDeltaTime, Space.Self);
        }
        
    }
}
