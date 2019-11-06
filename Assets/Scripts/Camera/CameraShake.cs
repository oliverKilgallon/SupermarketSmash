using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 origPos;
    float elapsed;
    float xDisp;
    float yDisp;

    public IEnumerator Shake(float dur, float magnitude)
    {
        origPos = transform.localPosition;
        elapsed = 0.0f;

        while (elapsed < dur)
        {
            xDisp += Random.Range(-1f, 1f) * magnitude;
            yDisp += Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(xDisp, yDisp, origPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = origPos;
        
    }
}
