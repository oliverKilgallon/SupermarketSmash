using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public List<string> localItems = new List<string>();
    public Text[] listText = new Text[8];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        int left = 8;
        foreach (Text t in listText)
        {
            if(localItems[i]== "" || localItems[i] == null)
            {
                t.text = "";
                left--;
            }
            else
            {
                t.text = localItems[i];
            }


            i++;
        }
        if (left == 0)
        {

            Debug.Log("your winner"); 
        }
    }
}
