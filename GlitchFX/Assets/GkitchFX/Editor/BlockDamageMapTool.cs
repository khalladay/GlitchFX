using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BlockDamageMapTool : EditorWindow
{

    private string[] textureSizeNames = new string[]{ "32", "64", "128", "256", "512", "1024", "2048" };
    private int[] textureSizes = new int[]{ 32, 64, 128, 256, 512, 1024, 2048 };

    private string[] blockSizeNames = new string[]{ "2", "4", "8", "16", "32", "64", "128", "256", "512", "1024", "2048" };
    private int[] blockSizes = new int[]{ 2,4,8,16, 32, 64, 128, 256, 512, 1024, 2048 };

    private int _curTextureSize = 256;
    private int _xBlockSizeMin = 8;
    private int _xBlockSizeMax = 64;

    private int _yBlockSizeMin = 8;
    private int _yBlockSizeMax = 64;

    private int _seed = 0;

    [MenuItem("KHGlitch/BlockDamageMapTool")]
    static void Init()
    {
        EditorWindow wnd = EditorWindow.GetWindow(typeof(BlockDamageMapTool));
        wnd.Show();
    }

    private void OnGUI()
    {
        bool isValid = true;
        GUI.enabled = true;

        EditorGUILayout.HelpBox("Block Damage Map Generator", MessageType.None);

        _curTextureSize = EditorGUILayout.IntPopup("Texture Size: ", _curTextureSize, textureSizeNames, textureSizes);


        EditorGUILayout.BeginHorizontal();
        _xBlockSizeMin = EditorGUILayout.IntPopup("Block Min Width", _xBlockSizeMin, blockSizeNames, blockSizes);
        _xBlockSizeMax = EditorGUILayout.IntPopup("Block Max Width", _xBlockSizeMax, blockSizeNames, blockSizes);

        EditorGUILayout.EndHorizontal();

        if (_xBlockSizeMin > _curTextureSize || _xBlockSizeMax > _curTextureSize)
        {
            EditorGUILayout.HelpBox("Error: Block dimensions cannot be larger than the generated texture",MessageType.Error);
            isValid = false;
        }

        if (_xBlockSizeMin > _xBlockSizeMax)
        {
            EditorGUILayout.HelpBox("Error: Min size must be smaller than max size", MessageType.Error);
            isValid = false;

        }

        EditorGUILayout.BeginHorizontal();
        _yBlockSizeMin = EditorGUILayout.IntPopup("Block Min Height", _yBlockSizeMin, blockSizeNames, blockSizes);
        _yBlockSizeMax = EditorGUILayout.IntPopup("Block Max Height", _yBlockSizeMax, blockSizeNames, blockSizes);

        EditorGUILayout.EndHorizontal();

        if (_yBlockSizeMin > _curTextureSize || _yBlockSizeMax > _curTextureSize)
        {
            EditorGUILayout.HelpBox("Error: Block dimensions cannot be larger than the generated texture", MessageType.Error);
            isValid = false;
        }

        if (_yBlockSizeMin > _yBlockSizeMax)
        {
            EditorGUILayout.HelpBox("Error: Min size must be smaller than max size", MessageType.Error);
            isValid = false;

        }

        _seed = EditorGUILayout.IntField("Random Seed", _seed);


        if (!isValid)
        {
            EditorGUILayout.HelpBox("Please Fix All Errors Before Generating A Texture", MessageType.Error);
            GUI.enabled = false;
        }
        if (GUILayout.Button("Generate"))
        {
            Texture2D outTex = new Texture2D(_curTextureSize, _curTextureSize, TextureFormat.ARGB32, false);
            Random.InitState(_seed);

            int x = 0;
            int y = 0;

            int rowHeight = (int)Random.Range(_yBlockSizeMin, _yBlockSizeMax);
            while (true)
            {
                int bWidth = (int)Random.Range(_xBlockSizeMin, _xBlockSizeMax);

                float col = Random.Range(0.0f, 1.0f);
                Color blockCol = new Color(col, col, col);

                if (bWidth + x > _curTextureSize) bWidth -= ((bWidth + x) - _curTextureSize);

                Color[] blockColors = new Color[bWidth * rowHeight];
                for (int i = 0; i < bWidth * rowHeight; ++i) blockColors[i] = blockCol;

                outTex.SetPixels(x, y, bWidth, rowHeight, blockColors);

                x += bWidth;
                if (x >= _curTextureSize)
                {
                    x = 0;
                    y += rowHeight;
                    if (rowHeight + y > _curTextureSize) rowHeight -= ((rowHeight + y) - _curTextureSize);

                }

                if (y >= _curTextureSize) break;
               
            }
            outTex.Apply();

            string outPath = EditorUtility.SaveFilePanel("Where to save", "", "mytexture"+_seed, "png");
            byte[] b = outTex.EncodeToPNG();
            File.WriteAllBytes(outPath, b);
            AssetDatabase.Refresh();

        }

    }
}
