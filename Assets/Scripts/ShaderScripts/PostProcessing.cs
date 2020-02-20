using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    [SerializeField]
    private Material postProcessMaterial;

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
            default:
                break;

        }
    }
}
