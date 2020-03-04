using System.Collections.Generic;
using UnityEngine;

public class LayerMaskChange : MonoBehaviour
{
    public Camera playerCamera;
    public string[] origItemList;
    public List<string> localItems;

    private void Awake()
    {
        origItemList = GetComponent<Playerscript>().NamesList;
        localItems = GetComponent<Playerscript>().localItems;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < origItemList.Length; i++)
        {
            for (int j = 0; j < localItems.Count; j++)
            {
                if (origItemList[i].Equals(localItems[j]))
                {
                    ShowLayer(localItems[j]);
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
        if (!(playerCamera.cullingMask == (playerCamera.cullingMask | (1 << LayerMask.NameToLayer(layerName)))) )
        {
            playerCamera.cullingMask |= 1 << LayerMask.NameToLayer(layerName);
        }
    }

    public void UpdateLayerMask(List<string> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
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
