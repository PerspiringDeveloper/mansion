using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostFX : MonoBehaviour
{
    public Material FXMaterial;
    private RenderTexture postRenderTexture;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (postRenderTexture == null) {
            postRenderTexture = new RenderTexture(src.width, src.height, 0, src.format); // width, height, depth buffer bits, RT format
        }

        Graphics.Blit(src, postRenderTexture); // source, destination, material, pass
        FXMaterial.SetTexture("_MainTex", postRenderTexture);
        Graphics.Blit(src, dst);
    }
}
