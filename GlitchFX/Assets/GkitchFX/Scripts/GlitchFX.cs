using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Camera))]
public class GlitchFX: MonoBehaviour
{
    public float glitchAmount = 0.0f;
    public float shiftMag = 0.0f;
    public Texture2D blockTexture;

    private Shader _glitchShader;
    private Material _glitchMat;
    private float _realGlitch;

	// Use this for initialization
	void Start ()
    {
        _glitchShader = Shader.Find("Hidden/GlitchFX/GlitchFX_Shift");
        _glitchMat = new Material(_glitchShader);
        _glitchMat.SetTexture("_GlitchMap", blockTexture);

        Invoke("UpdateRandom", 0.25f);
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _glitchMat);
    }

    void UpdateRandom()
    {
        _glitchMat.SetFloat("_GlitchRandom", Random.Range(-1.0f, 1.0f));
        Invoke("UpdateRandom", Random.Range(0.01f, 0.1f));

    }
    // Update is called once per frame
    void Update ()
    {
        glitchAmount = Mathf.Clamp(glitchAmount, 0.0f, 1.0f);
        _realGlitch = Random.Range(glitchAmount * 0.25f, glitchAmount);

        _glitchMat.SetFloat("_ShiftMag", shiftMag);
        _glitchMat.SetFloat("_GlitchAmount", glitchAmount);
	}
}
