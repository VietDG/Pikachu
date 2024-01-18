using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : SingletonMonoBehaviour<LevelData>
{
    public TextAsset textAsset;

    private LoadLevelFormData[] loadLevelFormData;

    private class LevelDataContain
    {
        public LoadLevelFormData[] loadLevelFormData;
    }

    public override void Awake()
    {
        base.Awake();

        var levelDataContent = JsonUtility.FromJson<LevelDataContain>(textAsset.text);
        loadLevelFormData = levelDataContent.loadLevelFormData;
    }

    public LoadLevelFormData GetLevelConfig(int level)
    {
        return loadLevelFormData[level - 1];
    }

    public MapData GetBoardData(int level)
    {
        int mapID = GetLevelConfig(level).shapeid;
        var mapData = JsonUtility.FromJson<MapData>(Resources.Load<TextAsset>("Text/Shape" + mapID.ToString()).text);

        return mapData;
    }
}
