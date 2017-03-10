using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GlitchFX: MonoBehaviour
{
    public enum BlockDamageType
    {
        Solid,
        HueShift,
        Tint,
        Shift
    };

    [HideInInspector]
    public bool fxEnabled = true;

    [HideInInspector]
    public float scale = 1.0f;

    [HideInInspector]
    public bool blockDamage = false;

    [HideInInspector]
    public BlockDamageType blockDamageType = BlockDamageType.Solid;

    [HideInInspector]
    public Color blockColor = Color.black;

    [HideInInspector]
    public float magnitude = 1.0f;

    [HideInInspector]
    public Texture2D blockTexture;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
