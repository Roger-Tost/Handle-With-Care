using UnityEngine;
using UnityEngine.UI;

public class  Scr_RenderTexture : MonoBehaviour
{
    public Camera sceneCamera;
    public RawImage rawImage;
    public RenderTexture sharedRenderTexture;

    void Start()
    {
        if (sceneCamera != null)
        {
            sceneCamera.targetTexture = sharedRenderTexture;
        }

        if (rawImage != null)
        {
            rawImage.texture = sharedRenderTexture;
        }
    }
}