using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Vector3[] localArc;
    public int arcStep =0;
    public float step;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {if (arcStep < 15)
        {
            transform.position = Vector3.MoveTowards(transform.position, localArc[arcStep], step);
            if (transform.position == localArc[arcStep])
            {
                arcStep++;
            }
        }
    }
}
