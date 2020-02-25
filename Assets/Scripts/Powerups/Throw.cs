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
    public bool emit;
    public GameObject[] Emiters;
    public GameObject[] JamEmiters;
    public string weapon;
    public GameObject jam;
    public float floorY;
    public bool reverse;
   

    // Start is called before the first frame update
    void Start()
    {
        //localArc = new Vector3[16];
        emit = false;
        reverse = false;
        rand = new Vector3(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (arcStep < 16)
        {
            if (itemChild)
            {
                itemChild.transform.Rotate(rand);
            }

            transform.position = Vector3.MoveTowards(transform.position, localArc[arcStep], step);
            if (transform.position == localArc[arcStep])
            {
                arcStep++;
            }
            if (itemChild)
            {
                if (arcStep == 14) { itemChild.GetComponent<Rigidbody>().useGravity = true; }

            }
        }
        else
        {
            if (itemChild)
            {
                itemChild.GetComponent<Rigidbody>().useGravity = true;
                itemChild.GetComponent<BoxCollider>().enabled = true;
            }

            //Destroy(itemChild);
            if (emit == false && weapon == "flour")
            {

                foreach (GameObject emit in Emiters)
                {
                    emit.GetComponent<ParticleSystem>().Play();
                }

                emit = true;

            }
            if (emit == false && weapon == "jam")
            {
                foreach (GameObject emit in JamEmiters)
                {
                    emit.GetComponent<ParticleSystem>().Play();
                }
                jam.gameObject.transform.SetPositionAndRotation(new Vector3(jam.gameObject.transform.position.x, floorY + 0.001f, jam.gameObject.transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0)));
                jam.GetComponent<MeshRenderer>().enabled = true;
                jam.GetComponent<BoxCollider>().enabled = true;


                emit = true;


            }
        }
        if ((jam.transform.lossyScale.x <= 2) && (jam.transform.lossyScale.z <= 2)&&!reverse)
        {

            if (emit && jam.GetComponent<MeshRenderer>().enabled == true)
            {
                jam.transform.localScale += new Vector3(0.07f, 0.07f, 0);
                // jam.transform.lossyScale.Scale(new Vector3(2, 0, 2));
            }
        }
        else { reverse = true; jam.GetComponent<LifeSpan>().emit = false;jam.GetComponent<LifeSpan>().parent = this.gameObject; }
        
    }
    // if ((jam.transform.lossyScale.x > 2) && (jam.transform.lossyScale.z > 2) && jam.GetComponent<MeshRenderer>().enabled == true){ jam.GetComponent<LifeSpan>().emit = false; emit = false; }
}
