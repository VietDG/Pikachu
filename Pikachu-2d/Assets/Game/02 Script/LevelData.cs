using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : SingletonMonoBehaviour<LevelData>
{
    public TextAsset textAsset;

    private LevelConfig[] levelConfigs;

    private class LevelDataConfigContainer
    {
        public LevelConfig[] levelConfigs;
    }

    public override void Awake()
    {
        base.Awake();

        var levelDataConfigContainer = JsonUtility.FromJson<LevelDataConfigContainer>(textAsset.text);
        levelConfigs = levelDataConfigContainer.levelConfigs;
    }

    public LevelConfig GetLevelConfig(int level)
    {
        return levelConfigs[level - 1];
    }

    public BoardConfig GetBoardData(int level)
    {
        int boardId = GetLevelConfig(level).shapeid;
        Debug.Log("Level - BoardId: " + level + "-" + boardId);
        var boardData = JsonUtility.FromJson<BoardConfig>(Resources.Load<TextAsset>("Text/Shape" + boardId.ToString()).text);

        return boardData;
    }
}
