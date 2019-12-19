using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float speed;
    public int currentRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(speed * Time.deltaTime, 0, 0);
    }
    void Rotate()
    {
        currentRot = Random.Range(1, 360);
    }
}
