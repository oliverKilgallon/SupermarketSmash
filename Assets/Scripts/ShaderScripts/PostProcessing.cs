using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    [SerializeField]
    private Material postProcessMaterial;
    [SerializeField]
    private float waveSpeed = 10;
    [SerializeField]
    private bool waveActive;

    private float waveDistance;

    private Camera cam;

    public enum Shaders
    {
        None,
        DepthWave,
        DepthNormals,
        Outline
    }
    public Shaders shader;

    private void Start()
    {
        cam = GetComponent<Camera>();
        switch (shader)
        {
            case Shaders.DepthWave:
                cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
                postProcessMaterial.shader = Shader.Find("Unlit/WaveShader");
                break;
            case Shaders.DepthNormals:
                cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.DepthNormals;
                postProcessMaterial.shader = Shader.Find("Unlit/DepthNormalShader");
                break;
            case Shaders.Outline:
                cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.DepthNormals;
                postProcessMaterial.shader = Shader.Find("Unlit/OutlineShader");
                break;
            default:
                break;

        }

    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch (shader)
        {
            case Shaders.DepthWave:
                postProcessMaterial.SetFloat("_WaveDistance", waveDistance);
                break;
            case Shaders.DepthNormals:
                Matrix4x4 viewToWorld = cam.cameraToWorldMatrix;
                postProcessMaterial.SetMatrix("_viewToWorld", viewToWorld);
                break;
            default:
                break;
        }


        Graphics.Blit(source, destination, postProcessMaterial);
    }

    private void Update()
    {
        switch (shader)
        {
            case Shaders.DepthWave:
                if (waveActive)
                {
                    waveDistance = waveDistance + waveSpeed * Time.deltaTime;
                }
                else
                {
                    waveDistance = 0;
                }
                break;
            default:
                break;

        }
    }
}
