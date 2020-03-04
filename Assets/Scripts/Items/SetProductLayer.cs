using UnityEngine;

public class SetProductLayer : MonoBehaviour
{
    public GameObject itemHighlight;
    // Start is called before the first frame update
    void Start()
    {
        string layerName = GetComponent<ItemScript>().product;
        itemHighlight.layer = LayerMask.NameToLayer(layerName);
    }
}
