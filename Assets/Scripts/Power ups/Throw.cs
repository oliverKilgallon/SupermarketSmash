using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Vector3[] localArc;
    public int arcStep =0;
    public float step;
    public GameObject itemChild;
    Vector3 rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = new Vector3(Random.Range(5, 20), Random.Range(5, 20), Random.Range(5, 20));
    }

    // Update is called once per frame
    void Update()
    {if (arcStep < 15)
        {
            itemChild.transform.Rotate(rand);
            transform.position = Vector3.MoveTowards(transform.position, localArc[arcStep], step);
            if (transform.position == localArc[arcStep])
            {
                arcStep++;
            }
        }
    }
}
