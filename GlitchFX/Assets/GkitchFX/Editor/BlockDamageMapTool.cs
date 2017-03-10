using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockDamageMapTool : EditorWindow {

    [MenuItem("KHGlitch/BlockDamageMapTool")]
    static void Init()
    {
        EditorWindow wnd = EditorWindow.GetWindow(typeof(BlockDamageMapTool));
        wnd.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Generate"))
        {

        }

    }
}
