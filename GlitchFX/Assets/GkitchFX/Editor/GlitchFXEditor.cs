using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlitchFX))]
public class GlitchFXEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GlitchFX fx = target as GlitchFX;

        EditorGUILayout.HelpBox("Global Glitch Controls", MessageType.None);

        fx.fxEnabled = EditorGUILayout.Toggle("Glitch FX Enabled", fx.fxEnabled);
        fx.scale = EditorGUILayout.FloatField("FX ResoltionScale", fx.scale);
        fx.scale = Mathf.Clamp(fx.scale, 0.01f, 1.0f);

        EditorGUILayout.HelpBox("Visual FX Controls", MessageType.None);
        fx.blockDamage = EditorGUILayout.Toggle("Block Damage", fx.blockDamage);
        if (fx.blockDamage)
        {
            fx.blockDamageType = (GlitchFX.BlockDamageType)EditorGUILayout.EnumPopup("  Damage Type", fx.blockDamageType);

            switch (fx.blockDamageType)
            {
                case GlitchFX.BlockDamageType.Solid:
                case GlitchFX.BlockDamageType.Tint:
                {
                    fx.blockColor = EditorGUILayout.ColorField("  Color", fx.blockColor);
                }break;
                case GlitchFX.BlockDamageType.HueShift:
                {

                }break;
                case GlitchFX.BlockDamageType.Shift:
                {
                    fx.magnitude = EditorGUILayout.FloatField("  Magnitude", fx.magnitude); 
                }break;
            }

        }

    }
}
