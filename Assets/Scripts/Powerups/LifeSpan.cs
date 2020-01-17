using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10) {
            gameObject.transform.localScale += new Vector3(-0.01f, 0,-0.01f);
            if ((gameObject.transform.localScale.x <= 0) && (gameObject.transform.localScale.z <= 0)) { Destroy(this.gameObject); }
        }
        
    }
}
