using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : SingletonMonoBehaviour<GameController>
{
    public UiGamePLayManager uiGamePlayManager;

    public BoosterManager boosterManager;

    public CameraController camController;

    public SpecialTile specialTile;

    public MatchTile matchTile;

    public SliderTile sliderTile;

    public ShuffleTile shuffleTile;

    public TileSpawn tileSpawn;

    private BoardConfig boardConfig;

    private LevelConfig levelConfig;

    private int totalLevel;

    private float time;

    private bool timeCount = true;

    private float[] starProgress = new float[] { 0.455f, 0.73f, 0.99f };


    public override void Awake()
    {
        Application.targetFrameRate = 60;

        EventAction.OnNextLevel += OnCLickNextLevel;

        EventAction.OnMatchTile += OnTileMatched;
        EventAction.WinGame += CheckWin;
        InitLevel();
    }

    public void InitLevel()
    {
        LoadLevelData();

        InitDataStart();

        camController.InitCam();

        StateGame.Play();
    }

    private void OnDestroy()
    {
        EventAction.OnNextLevel -= OnCLickNextLevel;

        EventAction.OnMatchTile -= OnTileMatched;
        EventAction.WinGame -= CheckWin;
    }

    private void LoadLevelData()
    {
        totalLevel = PlayerData.Instance.HighestLevel;

        if (totalLevel <= 1)
        {
            levelConfig = new LevelConfig()
            {
                time = -1
            };

            boardConfig = new BoardConfig()
            {
                containTileIndex = true,
                row = 5,
                col = 5,
                datas = new int[25]
                {
                    -1, 0, -1, 1, 2,
                    -1, 3, 1, 4, 5,
                    2, 6, -1, 6, 4,
                    7, 0, -1, -1, 3,
                    5, 7, -1, -1, -1 // vi tri button lv 1
                }
            };

        }
        else
        {
            levelConfig = LevelData.Instance.GetLevelConfig(totalLevel);
            boardConfig = LevelData.Instance.GetBoardData(totalLevel);

            //uiGamePlayManager.gameObject.SetActive(true);
            //boosterManager.gameObject.SetActive(true);
            //  sliderTile.SetSlider(levelConfig.up, levelConfig.down, levelConfig.left, levelConfig.right);

            if (totalLevel == 2)
            {
                levelConfig.time = 0;
            }
        }

        //  matchTile.isSpawnStars = totalLevel > 1;// neu level > 1 spawm sao qua moi man

        timeCount = levelConfig.time > 0;
    }

    private void InitDataStart()
    {
        uiGamePlayManager.InitLevel();
        uiGamePlayManager.SetStarCollect(0f, levelConfig.score);
        uiGamePlayManager.SetProgressStarCollected(starProgress);

        if (timeCount)
        {
            uiGamePlayManager.SetModeTime(true);
            time = levelConfig.time;
            uiGamePlayManager.InitTimeToLevel(levelConfig.time);
            uiGamePlayManager.SetTime(levelConfig.time);

            StartCoroutine(UpdateTime());
        }
        else
        {
            uiGamePlayManager.SetModeTime(false);
        }
        TileData.Instance.Initialize(levelConfig, boardConfig, 30);
        GameManager.Instance.OnSpawnTile(TileData.Instance);
        tileSpawn.StartSpawn();

        //  specialTile.HamOff();

        //if (totalLevel >= 16)
        //{
        //    levelConfig.hammer = 2;
        //}
        //else levelConfig.hammer = 0;

        //if (levelConfig.hammer > 0)
        //    specialTile.CreateHam(levelConfig.hammer);
    }

    private void OnTileMatched(MatchT match)
    {
        StartCoroutine(PosTileMatched());
    }

    private IEnumerator PosTileMatched()
    {
        yield return specialTile.StartMoveHam();

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

    private IEnumerator UpdateTime()
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

        InitDataStart();
    }

    public void OnCLickNextLevel()
    {
        StopAllCoroutines();

        SceneManager.LoadScene(Const.SCENE_GAME);
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
        totalLevel++;
        PlayerData.Instance.HighestLevel = totalLevel;
        int coin = 20;
        PlayerData.Instance.TotalCoin += coin;

        PopupWin.Instance.Show(coin);
    }

    public void SetLose()
    {
        StateGame.PauseGame();
        Debug.LogError("Lose Game");
        PopupLose.Instance.Show();
    }
}
