using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    public float timer;
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= life) {
            if (this.GetComponent<BoxCollider>()) { this.GetComponent<BoxCollider>().enabled = false; }
            gameObject.transform.localScale += new Vector3(-0.0001f, 0,-0.0001f);
            if ((gameObject.transform.lossyScale.x <= 0.06174203) && (gameObject.transform.lossyScale.z <= 0.06174203)) { Destroy(this.gameObject); }
        }
        
    }
}
