using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class ShakeTriggerTest : MonoBehaviour
{
    public CameraShake cameraShake;
    public float duration;
    public float magnitude;

    /**
     * If this code is need in the future it will be un-commented
    public void Awake()
    {
        //Checks if the camera shake script is currently on the main camera, then adds it if not
        if (!Camera.main.GetComponent<CameraShake>() && SceneManager.GetActiveScene().name.Equals("Main game"))
        {
            Camera.main.gameObject.AddComponent<CameraShake>();
        }
    }
    */
    void FixedUpdate()
    {
        /**Current input check is for testing, 
         * will be changed to appropriate function call as appropriate in the future
         */
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
