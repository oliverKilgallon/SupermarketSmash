using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public List<string> localItems = new List<string>();
    public Text[] listText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (Text t in listText)
        {
            t.text = localItems[i];
            i++;
        }
    }
}
