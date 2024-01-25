﻿using DG.Tweening;
using SS.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : SingletonMonoBehaviour<GameController>
{
    public UiGamePLayManager uiGamePlayManager;

    public TuttorialManager tuttorialManager;

    public BoosterManager boosterManager;

    public CameraController camController;

    public SpecialTile specialTile;

    public MatchTile matchTile;

    public SliderTile sliderTile;

    public ShuffleTile shuffleTile;

    public TileSpawn tileSpawn;

    private MapData mapData;

    private LoadLevelFormData loadLevelFormData;

    [SerializeField] Image _bg;

    public int totalLevel;

    public float time { get; set; }

    private bool timeCount = true;

    public override void Awake()
    {
        Application.targetFrameRate = 60;
        CameraDestroy.Instance.SetCam(false);

        EventAction.OnNextLevel += OnCLickNextLevel;

        EventAction.OnMatchTile += OnTileMatched;
        EventAction.OnSelectTile += OnSelectTile;
        EventAction.WinGame += CheckWin;
        SettingData.Instance.StateScence = StateScence.GamePlay;
    }

    public void InitLevel()
    {
        LoadLevelData();

        InitDataStart();

        camController.InitCam();

        SetBG();
    }

    private void OnSelectTile(ItemTile itemTile, bool isPlay)
    {
        if (isPlay)
        {
            if (!StateGame.IsPlay())
            {
                StateGame.Play();
            }
        }
    }


    public void SetBG()
    {
        _bg.sprite = BackGroundManager.Instance._themeList.GetBg();
    }

    private void OnDestroy()
    {
        EventAction.OnNextLevel -= OnCLickNextLevel;

        EventAction.OnMatchTile -= OnTileMatched;
        EventAction.OnSelectTile -= OnSelectTile;
        EventAction.WinGame -= CheckWin;
    }

    private void LoadLevelData()
    {
        totalLevel = PlayerData.Instance.HighestLevel;

        if (totalLevel <= 1)
        {
            loadLevelFormData = new LoadLevelFormData()
            {
                leveltime = -1
            };

            mapData = new MapData()
            {
                isShuf = true,
                row = 5,
                col = 5,
                datas = new int[25]
                {
                    -1, 0, -1, 1, 2,
                    -1, 3, 1, 4, 5,
                    2, 6, -1, 6, 4,
                    7, 0, -1, -1, 3,
                    5, 7, -1, -1, -1
                }
            };
            GameController.Instance.boosterManager.gameObject.SetActive(false);
            GameController.Instance.uiGamePlayManager.gameObject.SetActive(false);
        }
        else
        {
            loadLevelFormData = LevelData.Instance.GetLevelConfig(totalLevel);
            mapData = LevelData.Instance.GetBoardData(totalLevel);

            sliderTile.SetSlider(loadLevelFormData.moveup, loadLevelFormData.movedown, loadLevelFormData.moveleft, loadLevelFormData.moveright);

        }
        timeCount = loadLevelFormData.leveltime > 0;
    }

    private void InitDataStart()
    {
        uiGamePlayManager.InitLevel();

        if (timeCount)
        {
            uiGamePlayManager.SetModeTime(true);
            time = loadLevelFormData.leveltime;
            uiGamePlayManager.InitTimeToLevel(loadLevelFormData.leveltime);
            uiGamePlayManager.SetTime(loadLevelFormData.leveltime);

            StartCoroutine(UpdateTime());
        }
        else
        {
            uiGamePlayManager.SetModeTime(false);
        }
        TileData.Instance.Initialize(loadLevelFormData, mapData, 30);
        GameManager.Instance.OnSpawnTile(TileData.Instance);
        tileSpawn.StartSpawn();

        specialTile.HamOff();

        if (totalLevel >= 7)
        {
            loadLevelFormData.ham = 2;
        }
        else loadLevelFormData.ham = 0;

        if (loadLevelFormData.ham > 0)
            specialTile.CreateHam(loadLevelFormData.ham);

        tuttorialManager.SetTut(totalLevel);
    }

    private void OnTileMatched(MatchT match)
    {
        StartCoroutine(PosTileMatched());
    }

    private IEnumerator PosTileMatched()
    {
        yield return specialTile.StartMoveHam();

        // yield return new WaitForSeconds(0.5f);
        yield return sliderTile.StartGetSize();
        if (GameManager.Instance.FindAllTile() == null)
        {
            var shuffleData = GameManager.Instance.Shuffle();
            if (shuffleData != null)
            {
                yield return shuffleTile.StartShuffleTile(shuffleData.itemTiles, shuffleData.pos);
            }
        }
    }

    public IEnumerator UpdateTime()
    {
        yield return null;

        while (true)
        {
            if (StateGame.IsPlay())
            {
                time -= Time.deltaTime;
                if (time > 0f)
                {
                    uiGamePlayManager.SetTime(time);
                }
                else
                {
                    time = 0f;
                    uiGamePlayManager.SetTime(time);
                    break;
                }
            }

            yield return null;
        }
        SetLose();
    }

    public void OnClickReplay()
    {
        StopAllCoroutines();
        Manager.Load(DGame.SCENE_NAME);
    }

    public void OnCLickNextLevel()
    {
        StopAllCoroutines();

        Manager.Load(DGame.SCENE_NAME);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            CheckWin();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PopupLose.Instance.Show();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            PopupTutHamer.Instance.Show();
        }
    }

    public void CheckWin()
    {
        PlayerData.Save();
        StartCoroutine(WaitWin());
    }

    public IEnumerator WaitWin()
    {
        yield return new WaitForSeconds(1f);

        StateGame.PauseGame();
        PlayerData.Instance.TileSpriteIndex++;
        PlayerData.Instance.ThemeIndex++;

        totalLevel++;
        PlayerData.Instance.HighestLevel = totalLevel;

        int coin = 20;
        PlayerData.Instance.TotalCoin += coin;

        PopupWin.Instance.Show(coin);
    }

    public void SetLose()
    {
        StateGame.PauseGame();
        PopupLose.Instance.Show();
    }
}
