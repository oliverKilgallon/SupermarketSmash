using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGrounded : MonoBehaviour
{
    public Transform wheelPairCenter;
    private void Update()
    {
        Vector3 position = wheelPairCenter.TransformPoint(wheelPairCenter.position);
        Debug.DrawRay(position, Vector3.down, Color.red);
    }
}
