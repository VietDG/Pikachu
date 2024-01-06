using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;

public class DataExecutorWindow : EditorWindow
{
    [MenuItem("Window/DataExecutor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DataExecutorWindow));
    }

    private BoardConfig[] boardDatas = new BoardConfig[606];

    void OnGUI()
    {
        if (GUILayout.Button("Load"))
        {
            Load();
        }

        if (GUILayout.Button("Compress"))
        {
            Execute();
        }
    }

    private void Load()
    {
        for (int i = 0; i < boardDatas.Length; i++)
        {
            var text = Resources.Load<TextAsset>("Text/Map" + (i + 1).ToString());

            boardDatas[i] = JsonUtility.FromJson<BoardConfig>(text.text);

            var board = boardDatas[i];

            //for (int x = 0; x < board.datas.Length; x++)
            //{
            //    if (board.datas[x] != 0 && board.datas[x] != 1)
            //    {
            //        Debug.Log((i + 1) + "---" + board.datas[x]);
            //    }
            //}

            if (board.row == 7 && board.col == 11 && board.datas[0] == 0 && board.datas[1] == 1 && board.datas[2] == 0 && board.datas[3] == 0 && board.datas[4] == 0 && board.datas[5] == 1 && board.datas[6] == 0)
                Debug.Log(i + 1);


        }
    }

    void Execute()
    {
        string dataPath = "D:/Unity/Projects/Tiles Connect/";
        var aFilePaths = Directory.GetFiles("D:/Unity/Projects/Tiles Connect/Assets/Text");

        List<string> assetPaths = new List<string>();

        foreach (string sFilePath in aFilePaths)
        {
            if (!sFilePath.Contains(".meta"))
            {
                string assetPath = sFilePath.Substring(dataPath.Length, sFilePath.Length - dataPath.Length);
                string assetName = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath).name;

                assetPaths.Add(assetName.Substring(5));


            }
        }

        assetPaths.Sort(
            (x, y) =>
            {
                int a = int.Parse(x);
                int b = int.Parse(y);

                if (a > b)
                    return 1;
                else if (a < b)
                    return -1;
                else
                    return 0;
            });
        for (int i = 0; i < assetPaths.Count; i++)
        {
            AssetDatabase.RenameAsset("Assets/Text/Shape" + assetPaths[i] + ".txt", "Map" + (i + 1).ToString() + ".txt");
        }
    }
}
#endif
