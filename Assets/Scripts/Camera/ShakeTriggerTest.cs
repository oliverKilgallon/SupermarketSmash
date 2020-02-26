using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class ShakeTriggerTest : MonoBehaviour
{
    public CameraShake cameraShake;
    public float shakeThreshold = 10.0f;
    public float shakeDuration;
    public float shakeMagnitude;
    
    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.relativeVelocity.magnitude > shakeThreshold)
        {
            StartCoroutine(cameraShake.Shake(shakeDuration, collision.relativeVelocity.magnitude * shakeMagnitude));
        }*/
    }
}
