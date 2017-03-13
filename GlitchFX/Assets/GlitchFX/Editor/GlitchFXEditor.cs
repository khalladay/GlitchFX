using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(GlitchFX))]
//public class GlitchFXEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.DrawDefaultInspector();

//        GlitchFX fx = target as GlitchFX;

//        EditorGUILayout.HelpBox("Global Glitch Controls", MessageType.None);

//        fx.fxEnabled = EditorGUILayout.Toggle("Glitch FX Enabled", fx.fxEnabled);
//        fx.glitchAmount = EditorGUILayout.FloatField("Glitch Amount", fx.glitchAmount);
//        fx.glitchAmount = Mathf.Clamp(fx.glitchAmount, 0.0f, 1.0f);

//        EditorGUILayout.HelpBox("Visual FX Controls", MessageType.None);
//        fx.blockDamage = EditorGUILayout.Toggle("Block Damage", fx.blockDamage);
//        if (fx.blockDamage)
//        {
//            fx.blockTexture = EditorGUILayout.ObjectField("Block Damage Map: ", fx.blockTexture,typeof(Texture2D), true) as Texture2D;

//            fx.blockDamageType = (GlitchFX.BlockDamageType)EditorGUILayout.EnumPopup("  Damage Type", fx.blockDamageType);

//            switch (fx.blockDamageType)
//            {
//                case GlitchFX.BlockDamageType.Solid:
//                case GlitchFX.BlockDamageType.Tint:
//                {
//                    fx.blockColor = EditorGUILayout.ColorField("  Color", fx.blockColor);
//                }break;
//                case GlitchFX.BlockDamageType.HueShift:
//                {

//                }break;
//                case GlitchFX.BlockDamageType.Shift:
//                {
//                    fx.blockMagnitude = EditorGUILayout.FloatField("  Magnitude", fx.blockMagnitude); 
//                }break;
//            }

//        }

//    }
//}
