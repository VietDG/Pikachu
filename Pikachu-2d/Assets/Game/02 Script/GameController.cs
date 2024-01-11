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

    //  private PlayerData userData;

    public override void Awake()
    {
        Application.targetFrameRate = 60;//Application.targetFrameRate: làm trò chơi chạy nhanh hơn 

        //  UserData.Load();// load vàng , tên ,decor,level

        EventAction.OnNextLevel += OnCLickNextLevel;

        EventAction.OnMatchTile += OnTileMatchSucceeded;
        EventAction.WinGame += HandleGameWin;
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

        EventAction.OnMatchTile -= OnTileMatchSucceeded;
        EventAction.WinGame -= HandleGameWin;
    }

    private void LoadLevelData()
    {
        totalLevel = PlayerData.playerData.userProfile.totalLevel;

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

            // uiGamePlayManager.gameObject.SetActive(false);
            // boosterManager.gameObject.SetActive(false);// tat top bot lv1
        }
        else // neu lv>1 thi 
        {
            levelConfig = LevelData.Instance.GetLevelConfig(totalLevel);//load level tu data
            boardConfig = LevelData.Instance.GetBoardData(totalLevel);//load bang level

            uiGamePlayManager.gameObject.SetActive(true);// active top down
            boosterManager.gameObject.SetActive(true);
            sliderTile.SetSlider(levelConfig.up, levelConfig.down, levelConfig.left, levelConfig.right);

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
        uiGamePlayManager.SetStarCollect(0f, levelConfig.score);//hien level
        uiGamePlayManager.SetProgressStarCollected(starProgress);// moc tien trinh

        if (timeCount)
        {
            uiGamePlayManager.SetModeTime(true);
            time = levelConfig.time;
            uiGamePlayManager.InitTimeToLevel(levelConfig.time);
            uiGamePlayManager.SetTime(levelConfig.time);

            StartCoroutine(UpdateTime());// khởi động game time cout sẽ chạy
                                         // chay thời gian còn lại trong level
        }
        else
        {
            uiGamePlayManager.SetModeTime(false);// level k có time thì k bật 
        }
        TileData.Instance.Initialize(levelConfig, boardConfig, 30);// khởi tạo mảng daata gồm cấp độ 
        GameManager.Instance.OnSpawnTile(TileData.Instance);// tajp barng
        tileSpawn.StartSpawn();

        specialTile.HamOff();

        if (totalLevel >= 16)
        {
            levelConfig.hammer = 2;// tạo 2 búa
        }
        else levelConfig.hammer = 0;

        if (levelConfig.hammer > 0)
            specialTile.CreateHam(levelConfig.hammer);// tạo 1 búa
    }

    private void OnTileMatchSucceeded(MatchT match)
    {
        StartCoroutine(PostTileMatchSucceededSchedule());// sắp xếp thành công
    }

    private IEnumerator PostTileMatchSucceededSchedule()
    {
        yield return specialTile.StartMoveHam();// spawm boom+búa

        yield return sliderTile.StartGetSize();// effect viên gạch

        if (GameManager.Instance.FindAllTile() == null)
        {
            var shuffleData = GameManager.Instance.Shuffle();// trộn gạch
            if (shuffleData != null)
            {
                yield return shuffleTile.PlayEffect(shuffleData.itemTiles, shuffleData.pos);// effect gạch
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

        HandleGameLose();
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
            HandleGameWin();
            Debug.LogError("Win");
        }
    }

    public void HandleGameWin()
    {
        //StateGame.PauseGame();
        //totalLevel++;
        //PlayerData.playerData.userProfile.totalLevel = totalLevel;
        //int coin = 20;
        //PlayerData.playerData.userProfile.totalCoin += coin;

        //PopupWin.Instance.Show(coin);
        ////    StateGame.NextLevels();
        StartCoroutine(WaitWin());
    }

    public IEnumerator WaitWin()
    {
        yield return new WaitForSeconds(2f);
        StateGame.PauseGame();
        totalLevel++;
        PlayerData.playerData.userProfile.totalLevel = totalLevel;
        int coin = 20;
        PlayerData.playerData.userProfile.totalCoin += coin;

        PopupWin.Instance.Show(coin);
        //    StateGame.NextLevels();
    }

    public void HandleGameLose()
    {
        StateGame.PauseGame();
    }


}
