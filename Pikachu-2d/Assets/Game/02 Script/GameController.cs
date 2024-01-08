using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TopPanelView topPanelView;

    //public BoosterViewManager boosterView;

    public CameraController cameraAligner;

    public FeatureSpawner featureSpawner;

    //  public TutorialController tutorialController;

    public TileMatchEffect tileMatchEffect;

    public TileSliderEffect tileSlideEffect;

    public TileShuffleEffect tileShuffleEffect;

    public SpawnTileEffect tileSpawnEffect;

    // private TileData boardData => TileData.Instance;

    private BoardConfig boardConfig;

    private LevelConfig levelConfig;

    private int level;

    private float remainingTime;

    private bool timeCount = true;

    private float[] starProgressMilestone = new float[] { 0.455f, 0.73f, 0.99f };

    private UserData userData;

    private void Awake()
    {
        Application.targetFrameRate = 60;//Application.targetFrameRate: làm trò chơi chạy nhanh hơn 


        //  UserData.Load();// load vàng , tên ,decor,level

        GamePlayState.GamePostRestartEvent += Replay;// chơi lại
        GamePlayState.GameNextLevelEvent += NextLevel;
        GamePlayState.GamePauseEvent += GamePause;
        GamePlayState.GameContinueEvent += GameContinue;

        TileMatchManager.TileSelectedEvent += OnTileSelected;// event click vào ô trên bảng
        TileMatchManager.TileMatchSucceededEvent += OnTileMatchSucceeded;

        bool sfxEnabled = PlayerPrefs.GetInt("sfx", 1) == 1;// tùy chọn âm thanh
        bool bgmEnabled = PlayerPrefs.GetInt("bgm", 1) == 1;// tùy chọn sound

        GameManager.Instance.OnBoardClear += HandleGameWin;//win game + level

        //boardData = BoardTileData.Instance;// bảng xáo trộn qua mỗi màn 

        LoadLevel();

        StartGame();

        cameraAligner.Initialize();
    }

    private void Start()
    {

    }



    private void OnDestroy()
    {
        GamePlayState.GamePostRestartEvent -= Replay;
        GamePlayState.GameNextLevelEvent -= NextLevel;
        GamePlayState.GamePauseEvent -= GamePause;
        GamePlayState.GameContinueEvent -= GameContinue;

        TileMatchManager.TileSelectedEvent -= OnTileSelected;
        TileMatchManager.TileMatchSucceededEvent -= OnTileMatchSucceeded;

    }

    private void Update()
    {

    }

    private void LoadLevel()
    {
        level = UserData.current.userStatus.level;

        if (level <= 1)
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

            topPanelView.gameObject.SetActive(false);
            //  boosterView.gameObject.SetActive(false);// tat top bot lv1
        }
        else // neu lv>1 thi 
        {
            levelConfig = LevelDataLoader.Instance.GetLevelConfig(level);//load level tu data
            boardConfig = LevelDataLoader.Instance.GetBoardData(level);//load bang level

            topPanelView.gameObject.SetActive(true);// active top down
            //  boosterView.gameObject.SetActive(true);
            tileSlideEffect.SetSlideOrder(levelConfig.up, levelConfig.down, levelConfig.left, levelConfig.right);
            // hieu ung tren gach
            if (level == 2)
            {
                levelConfig.time = 0;
            }
        }

        tileMatchEffect.spawnStar = level > 1;// neu level > 1 spawm sao qua moi man

        timeCount = levelConfig.time > 0;

        //  Debug.Log(levelConfig.boom + "-" + levelConfig.score);
    }

    private void StartGame()
    {

        topPanelView.SetLevel(UserData.current.userStatus.level);//hien ra level tu user data
        topPanelView.SetCollectedStar(0f, levelConfig.score);//hien level
        topPanelView.SetCollectedStarProgressMilestone(starProgressMilestone);// moc tien trinh

        if (timeCount)
        {
            topPanelView.SetTimeCount(true);
            remainingTime = levelConfig.time;
            topPanelView.SetLevelTime(levelConfig.time);
            topPanelView.SetRemainingTime(levelConfig.time);

            StartCoroutine(UpdateTime());// khởi động game time cout sẽ chạy
                                         // chay thời gian còn lại trong level

        }
        else
        {
            topPanelView.SetTimeCount(false);// level k có time thì k bật 
        }


        TileData.Instance.Initialize(levelConfig, boardConfig, 30);// khởi tạo mảng daata gồm cấp độ 
        GameManager.Instance.SpawnTiles(TileData.Instance);// tajp barng
        tileSpawnEffect.Play();

        //   featureSpawner.ResetAllPools();

        if (levelConfig.boom > 0)
            //    featureSpawner.CreateBomb(levelConfig.boom);// spam boom

            if (hammerLevel.Contains(level) || level >= 16)
            {
                levelConfig.hammer = 2;// tạo 2 búa
            }
            else levelConfig.hammer = 0;

        if (levelConfig.hammer > 0) ;
        //   featureSpawner.CreateHammer(levelConfig.hammer);// tạo 1 búa


        //AdvertiseManager.Instance.ShowBanner();
    }

    private List<int> hammerLevel = new List<int>() { 7, 8, 10, 12, 13, 15 };

    private void OnTileSelected(ItemTile tile, bool selected)
    {
        if (selected)
        {
            if (!GamePlayState.IsPlaying())
            {
                GamePlayState.Play();
            }
        }
    }

    private void OnTileMatchSucceeded(Match match)
    {
        StartCoroutine(PostTileMatchSucceededSchedule());// sắp xếp thành công
    }

    private IEnumerator PostTileMatchSucceededSchedule()
    {
        yield return featureSpawner.PostTileMatchSucceeded();// spawm boom+búa

        yield return tileSlideEffect.PlayCoroutine();// effect viên gạch

        if (GameManager.Instance.CheckAnyMatch() == null)
        {
            var shuffleData = GameManager.Instance.Shuffle();// trộn gạch
            if (shuffleData != null)
            {
                yield return tileShuffleEffect.PlayEffect(shuffleData.tiles, shuffleData.positions);// effect gạch
            }
        }
    }

    private IEnumerator UpdateTime()
    {
        yield return null;

        while (true)
        {
            if (GamePlayState.IsPlaying())
            {
                remainingTime -= Time.deltaTime;

                if (remainingTime > 0f)
                {
                    topPanelView.SetRemainingTime(remainingTime);
                }
                else
                {
                    remainingTime = 0f;
                    topPanelView.SetRemainingTime(remainingTime);
                    break;
                }
            }

            yield return null;
        }

        HandleGameLose();
    }

    public void Replay()
    {
        StopAllCoroutines();

        StartGame();
    }

    public void NextLevel()
    {
        StopAllCoroutines();

        LoadLevel();

        GamePlayState.Pause();

        StartGame();

        cameraAligner.Initialize();// chưa đọc 
    }

    public void GamePause()
    {

        //AdvertiseManager.Instance.HideBanner();
    }

    public void GameContinue()
    {

        //AdvertiseManager.Instance.ShowBanner();
    }

    public void HandleGameWin()
    {
        //    GamePlayState.Pause();

        level++;
        UserData.current.userStatus.level = level;
        UserData.current.decorData.tilePackIndex++;

        //if (level != 2)
        //{
        //    var goldPigData = UserData.current.goldPigData;// lấy dữ liệu con lợn vàng
        //    int goldPigBonusCoin = UnityEngine.Random.Range(20, 25);
        //    int goldPigPreviousCoin = goldPigData.coinAmount;
        //    var stashRange = GoldPigUtility.GetSmashRange();
        //    goldPigData.coinAmount = Mathf.Min(goldPigData.coinAmount + goldPigBonusCoin, stashRange.Item2);
        //    var userStatus = UserData.current.userStatus;//
        //    userStatus.coinCount += goldPigBonusCoin;//
        //    EventDispatcher.Instance.NotifyEvent("coin_update", userStatus.coinCount);// + gold mỗi màn

        //    //    topPanelView.GetCollectedStar(out float collectedStartAmount, out float maxAmount);
        //    //  float percent = collectedStartAmount / maxAmount;

        //    int collectStartCount = 0;
        //    //if (percent >= starProgressMilestone[0]) collectStartCount++;
        //    //if (percent >= starProgressMilestone[1]) collectStartCount++;
        //    //if (percent >= starProgressMilestone[2]) collectStartCount++;

        //}
        //else
        //{
        GamePlayState.NextLevel();
        //    }

        //if (level >= 3)
        //{
        //}
    }

    public void HandleGameLose()
    {
        GamePlayState.Pause();
    }


}
