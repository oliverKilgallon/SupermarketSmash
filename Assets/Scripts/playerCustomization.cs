using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerCustomization : MonoBehaviour
{
    //GameObject[] players = new GameObject[4];
    public Slider R, G, B;
    public Color modelColor;
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        //Object.DontDestroyOnLoad(this.gameObject);
        R.minValue = 0;
        R.maxValue = 1;
        G.minValue = 0;
        G.maxValue = 1;
        B.minValue = 0;
        B.maxValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("R: " + R.value + ", G: " + G.value + ", B: " + B.value);
        modelColor.r = R.value;
        modelColor.g = G.value;
        modelColor.b = B.value;
        modelColor.a = 255;
        //Debug.Log(modelColor);
        GetComponent<RawImage>().color = modelColor;
        model.GetComponentInChildren<Renderer>().material.color = modelColor;
    }
}
