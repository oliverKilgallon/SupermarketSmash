using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    public float timer;
    public int life;
    public bool emit;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        emit = true;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= life) {
            if (this.GetComponent<BoxCollider>()) { this.GetComponent<BoxCollider>().enabled = false; }
            gameObject.transform.localScale += new Vector3(-0.07f,-0.07f,0);
            if ((gameObject.transform.localScale.x <= 0) && (gameObject.transform.localScale.y <= 0)) { Destroy(parent); }
        }
        
    }
}
