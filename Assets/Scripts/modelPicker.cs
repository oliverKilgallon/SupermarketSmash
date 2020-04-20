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
    public GameObject Display;

    public List<Material> materials = new List<Material>();

    private int meshIndex = 0;
    private int matIndex = 0;

    private void Update()
    {
        Display.GetComponent<MeshFilter>().mesh = meshes[meshIndex];
    }

    public void nextMesh()
    {
        if (meshIndex < meshes.Count - 1)
        {
            meshIndex++;
        }
        else
        {
            meshIndex = 0;
        }
    }

    public void previousMesh()
    {
        if (meshIndex > 0)
        {
            meshIndex--;
        }
        else
        {
            meshIndex = meshes.Count - 1;
        }
    }

    public void nextMaterial()
    {
        if (matIndex < materials.Count - 1)
        {
            matIndex++;
        }
        else
        {
            matIndex = 0;
        }
    }

    public void previousMaterial()
    {
        if (matIndex > 0)
        {
            matIndex--;
        }
        else
        {
            matIndex = materials.Count - 1;
        }
    }


}
