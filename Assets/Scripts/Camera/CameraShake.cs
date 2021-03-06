﻿using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 origCamPos;
    float elapsedTime;
    float xDisp;
    float yDisp;

    public IEnumerator Shake(float shakeDur, float shakeMagnitude)
    {
        origCamPos = transform.localPosition;
        elapsedTime = 0.0f;

        while (elapsedTime < shakeDur)
        {
            xDisp += Random.Range(-1f, 1f) * shakeMagnitude;
            yDisp += Random.Range(-1f, 1f) * shakeMagnitude;
            
            transform.localPosition = new Vector3(xDisp, yDisp, origCamPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = origCamPos;
        
    }
}
