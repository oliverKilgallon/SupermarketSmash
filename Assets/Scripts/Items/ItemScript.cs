using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public string product;
    public Mesh itemMesh;
    public Material itemMat; 
   
        
    public bool inWall;
   
    // Use this for initialization
    void Start()
    {
      //  product = "EMPTY";
        inWall = false;
        // product = Random.Range(0, 15);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            inWall = true;
        }
    }
    public IEnumerator enableColliders(GameObject item, float duration)
    {
        yield return new WaitForSeconds(duration);
        foreach (Collider c in item.GetComponents<Collider>())
        {
            c.enabled = true;
        }
    }
}
