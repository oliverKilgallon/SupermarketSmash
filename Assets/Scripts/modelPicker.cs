using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class modelPicker : MonoBehaviour
{
    public List<GameObject> meshes = new List<GameObject>();
    public Button forward;
    public Button Backward;
    public RawImage background;
    public GameObject Display;
    Vector3 loc;
    Quaternion rot;

    public List<Material> modelPickerMats = new List<Material>();
    Material[] MeshRendererMats;



    private int meshIndex = 0;
    private int matIndex = 0;

    private void Start()
    {
        MeshRendererMats = Display.GetComponentInChildren<SkinnedMeshRenderer>().materials;
        loc = Display.transform.position;
        rot = Display.transform.rotation;
        updateDisplay();
    }


    void updateDisplay()
    {
        Destroy(Display.gameObject);
        Display = Instantiate(meshes[meshIndex], loc, rot);
        Display.AddComponent<dontDestroy>();
        MeshRendererMats = Display.GetComponentInChildren<SkinnedMeshRenderer>().materials;
        MeshRendererMats[0] = modelPickerMats[matIndex];
        Display.GetComponentInChildren<SkinnedMeshRenderer>().materials = MeshRendererMats;
    }




    public void nextMesh()
    {
        if (meshIndex < meshes.Count - 1)
        {
            meshIndex++;
            updateDisplay();
        }
        else
        {
            meshIndex = 0;
            updateDisplay();
        }
    }

    public void previousMesh()
    {
        if (meshIndex > 0)
        {
            meshIndex--;
            updateDisplay();
        }
        else
        {
            meshIndex = meshes.Count - 1;
            updateDisplay();
        }
    }

    public void nextMaterial()
    {
        Debug.Log("mesh1");
        if (matIndex < modelPickerMats.Count - 1)
        {
            matIndex++;
            updateDisplay();
        }
        else
        {
            matIndex = 0;
            updateDisplay();
        }
    }

    public void previousMaterial()
    {
        Debug.Log("mesh2");
        if (matIndex > 0)
        {
            matIndex--;
            updateDisplay();
        }
        else
        {
            matIndex = modelPickerMats.Count - 1;
            updateDisplay();
        }
    }


}
