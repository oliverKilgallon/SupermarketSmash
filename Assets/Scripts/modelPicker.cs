using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class modelPicker : MonoBehaviour
{
    public List<Mesh> meshes = new List<Mesh>();
    public Button forward;
    public Button Backward;
    public RawImage background;
    public GameObject TEST;

    private int index = 0;

    private void Update()
    {
        TEST.GetComponent<MeshFilter>().mesh = meshes[index];
    }

    public void nextMesh()
    {
        if (index < meshes.Count - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }

    public void previousMesh()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = meshes.Count - 1;
        }
    }

}
