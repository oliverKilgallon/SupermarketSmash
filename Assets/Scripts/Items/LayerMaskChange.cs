using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskChange : MonoBehaviour
{
    public Camera playerCamera;
    private int origCullingMask;
    private string[] origItemList;
    private List<string> localItems;

    private void Awake()
    {
        string line;
        int index = 1;
        System.IO.StreamReader file =
           new System.IO.StreamReader("Assets/Text Files/ItemStorage.txt");
        while ((line = file.ReadLine()) != null)
        {
            origItemList[index] = line;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        origCullingMask = playerCamera.cullingMask;
        localItems = GetComponent<Playerscript>().localItems;
        for (int i = 0; i < origItemList.Length; i++)
        {
            for (int j = 0; j < localItems.Count; j++)
            {
                if (localItems[j].Equals(origItemList[i]))
                {
                    ShowLayer(origItemList[i]);
                }
            }
        }
    }

    public void ToggleLayer(string layerName)
    {
        playerCamera.cullingMask ^= 1 << LayerMask.NameToLayer(layerName);
    }

    public void HideLayer(string layerName)
    {
        if (playerCamera.cullingMask == (playerCamera.cullingMask | LayerMask.NameToLayer(layerName)))
        {
            playerCamera.cullingMask ^= ~(1 << LayerMask.NameToLayer(layerName));
        }
    }

    public void ShowLayer(string layerName)
    {
        if (!(playerCamera.cullingMask == (playerCamera.cullingMask | LayerMask.NameToLayer(layerName))))
        {
            playerCamera.cullingMask |= 1 << LayerMask.NameToLayer(layerName);
        }
    }

    public void UpdateLayerMask(string[] itemList)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (!itemList[i].Equals(""))
            {
                HideLayer(itemList[i]);
            }
            else
            {
                ShowLayer(itemList[i]);
            }
        }
    }
}
