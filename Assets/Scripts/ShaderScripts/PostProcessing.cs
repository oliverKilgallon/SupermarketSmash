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
        Outline,
        Blur
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
            case Shaders.Blur:
                postProcessMaterial.shader = Shader.Find("Unlit/BlurShader");
                break;
            default:
                break;

        }

    }

    //[ImageEffectOpaque]
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch (shader)
        {
            case Shaders.DepthWave:
                postProcessMaterial.SetFloat("_WaveDistance", waveDistance);
                Graphics.Blit(source, destination, postProcessMaterial);
                break;
            case Shaders.DepthNormals:
                Matrix4x4 viewToWorld = cam.cameraToWorldMatrix;
                postProcessMaterial.SetMatrix("_viewToWorld", viewToWorld);
                Graphics.Blit(source, destination, postProcessMaterial);
                break;
            case Shaders.Blur:
                RenderTexture tempTex = RenderTexture.GetTemporary(source.width, source.height);
                Graphics.Blit(source, tempTex, postProcessMaterial, 0);
                Graphics.Blit(tempTex, destination, postProcessMaterial, 1);
                RenderTexture.ReleaseTemporary(tempTex);
                break;
            default:
                Graphics.Blit(source, destination, postProcessMaterial);
                break;
        }


        
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
