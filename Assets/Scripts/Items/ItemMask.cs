using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemMask : MonoBehaviour
{
    public Camera playerCamera;
    public string mainGameScene;
    private int cameraOrigCullingMask;

    private void Awake()
    {
        SceneManager.sceneLoaded -= UpdateLayerMaskOnSceneLoad;
        SceneManager.sceneLoaded += UpdateLayerMaskOnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= UpdateLayerMaskOnSceneLoad;
    }
    private void Start()
    {
        cameraOrigCullingMask = playerCamera.cullingMask;
    }

    private void UpdateLayerMaskOnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name.Equals(mainGameScene))
        {
            //TODO - Get all items in list currently, set culling mask accordingly
        }
    }

    public void ToggleLayer(string layerName)
    {
        playerCamera.cullingMask ^= 1 << LayerMask.NameToLayer(layerName);
    }

    public void HideLayer(string layerName)
    {
        playerCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(layerName));
    }

    public void ShowLayer(string layerName)
    {
        playerCamera.cullingMask |= 1 << LayerMask.NameToLayer(layerName);
    }
}
