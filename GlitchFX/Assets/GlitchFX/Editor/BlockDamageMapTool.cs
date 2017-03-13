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


    private bool _separateGBChannels = false;
    private float _gMin = 0.0f;
    private float _gMax = 1.0f;
    private float _bMin = 0.0f;
    private float _bMax = 1.0f;

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

        EditorGUILayout.HelpBox("Block Damage Map Generator", MessageType.Info);
        EditorGUILayout.HelpBox("Block Layout Settings", MessageType.None);

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


        EditorGUILayout.HelpBox("Color Settings", MessageType.None);

        _separateGBChannels = EditorGUILayout.Toggle("Separate GB Channels", _separateGBChannels);

        if (_separateGBChannels) GUI.enabled = true;
        else GUI.enabled = false;

        {
            EditorGUILayout.BeginHorizontal();

            _gMin = EditorGUILayout.FloatField("G Min:", _gMin);
            _gMax = EditorGUILayout.FloatField("G Max:", _gMax);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            _bMin = EditorGUILayout.FloatField("B Min:", _bMin);
            _bMax = EditorGUILayout.FloatField("B Max:", _bMax);
            EditorGUILayout.EndHorizontal();
        }

        GUI.enabled = true;

        _seed = EditorGUILayout.IntField("Random Seed", _seed);

        if (!isValid)
        {
            EditorGUILayout.HelpBox("Please Fix All Errors Before Generating A Texture", MessageType.Error);
            GUI.enabled = false;
        }
        if (GUILayout.Button("Generate"))
        {
            Texture2D outTex = new Texture2D(_curTextureSize, _curTextureSize, TextureFormat.RGBAFloat, false);
            outTex.filterMode = FilterMode.Point;
            Random.InitState(_seed);

            int x = 0;
            int y = 0;

            int rowHeight = (int)Random.Range(_yBlockSizeMin, _yBlockSizeMax);
            while (true)
            {
                int bWidth = (int)Random.Range(_xBlockSizeMin, _xBlockSizeMax);

                float r = Random.Range(0.0f, 1.0f);

                float g = _gMin == _gMax ? _gMin :  Mathf.Pow(Random.Range(_gMin, _gMax), 1.0f);
                float b = _bMin == _bMax ? _bMin : Mathf.Pow(Random.Range(_bMin, _bMax), 1.0f);

                if (!_separateGBChannels)
                {
                    g = r;
                    b = r;
                }

                Color blockCol = new Color(r, g, b);

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
            byte[] bytes = outTex.EncodeToPNG();
            File.WriteAllBytes(outPath, bytes);
            AssetDatabase.Refresh();

        }

    }
}
