using UnityEngine;

public class AppConfig : SingletonMonoBehaviour<AppConfig>
{
    #region Default Value
    [Header("--- GAME CONFIGS ---")]
    [SerializeField] private int _showBonusButtonInEveryLevel = 3;
    [SerializeField] private int _videoCompletedToShowAd = 3;
    [SerializeField] private int _levelCompletedToShowAd = 4;
    [SerializeField] private int _adFrequencyTime = 90;

    public int ShowBonusButtonInEveryLevel
    {
        get { return _showBonusButtonInEveryLevel; }
        set { _showBonusButtonInEveryLevel = value; }
    }

    public int VideoCompletedToShowAd
    {
        get { return _videoCompletedToShowAd; }
        set { _videoCompletedToShowAd = value; }
    }

    public int LevelCompletedToShowAd
    {
        get { return _levelCompletedToShowAd; }
        set { _levelCompletedToShowAd = value; }
    }

    public int AdFrequencyTime
    {
        get { return _adFrequencyTime; }
        set { _adFrequencyTime = value; }
    }

    public bool CanShowInter => PlayerData.Instance.HighestLevel > LevelCompletedToShowAd;
    public bool CanShowBanner = false;

    [Header("PRICES")]
    [Header("CONFIG DEFAULT VALUE")]
    [Space(10)]
    public int initialBannerAdLevel = 0;
    public int interFrequencyTime = 0;
    // [SerializeField] private string _welcomeText = Const.KEY_WELCOM;
    //  public string WelcomeText { get => _welcomeText; }

    private bool _isInterActive = false;
    public bool IsInterActive { get => _isInterActive; }

    /* [Header("Noti Event")]
     [Space(10)]
     [SerializeField] string _content;
     [SerializeField] int _idContent;
     [SerializeField] string _rewards;
     [SerializeField] string _times;
     public string Content { get => _content; }
     public int IdContent { get => _idContent; }
     public string Rewards { get => _rewards; }
     public string Times { get => _times; }*/
    #endregion

    #region Value Fetch
    [Header("VALUE IS FETCH")]
    [Space(10)]
    public int INITIAL_BANNER_AD_LEVEL = 0;
    public int INTER_FEQUENCY_TIME = 0;
    //  public string WELCOM_TEXT;
    public bool IS_INTER_ACTIVE;
    /// <summary>
    /// Noti Event
    /// </summary>
   /* public string CONTENT;
    public int ID_CONTENT;
    public string REWARDS;
    public string TIMES;*/
    #endregion


    #region Unity Methods
    public override void Awake()
    {
        SetDefaultConfigValue();
    }

    private void Start()
    {

    }
    #endregion

    #region Private Methods
    public void SetDefaultConfigValue()
    {
        INITIAL_BANNER_AD_LEVEL = initialBannerAdLevel;

        INTER_FEQUENCY_TIME = interFrequencyTime;

        //  WELCOM_TEXT = WelcomeText;

        IS_INTER_ACTIVE = _isInterActive;

        //Event Noti
        /*  this.ID_CONTENT = _idContent;
          this.CONTENT = _content;
          this.REWARDS = _rewards;
          this.TIMES = _times;*/
    }
    #endregion

    private void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.F))
         {
             ID_CONTENT = _idContent;
             PlayerData.Instance.IdEventCurrent = ID_CONTENT;
         }*/
    }
}

