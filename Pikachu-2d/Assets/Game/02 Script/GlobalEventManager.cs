using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    #region Instance
    private static GlobalEventManager instance;
    public static GlobalEventManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GlobalEventManager>();
            }
            return instance;
        }
    }
    public static bool Exist => instance;
    #endregion

    public static Action<string, Parameter[]> EvtSendEvent;
    public static Action EvtUpdateUserProperties;

    #region GAMEPLAY


    private void Awake()
    {
        EventAction.EventTrackLevelPlay += OnTrackLevelPlay;//level
        EventAction.EventTrackLevelWin += OnTrackLevelWin;//win game
        EventAction.EventTrackLoseLevel += OnTrackLevelLose;//lose game
        EventAction.EventtTotalAdInterShown += OnTrackAdInterShow;//show inter
        EventAction.EventRewardShow += OnTrackRewadShow;//show reward
        EventAction.EventRewardComplete += OnTrackRewadComplete;//reward complete
        EventAction.EventInterClose += OnTrackAdInterClose;//close Inter
    }

    private void OnDestroy()
    {
        EventAction.EventTrackLevelPlay -= OnTrackLevelPlay;
        EventAction.EventTrackLevelWin -= OnTrackLevelWin;
        EventAction.EventTrackLoseLevel -= OnTrackLevelLose;
        EventAction.EventtTotalAdInterShown -= OnTrackAdInterShow;
        EventAction.EventRewardShow -= OnTrackRewadShow;
        EventAction.EventRewardShow -= OnTrackRewadComplete;
        EventAction.EventInterClose -= OnTrackAdInterClose;
    }

    private void OnTrackLevelPlay()
    {
        Parameter[] _params = new Parameter[]
        {
            new Parameter("level",PlayerData.Instance.HighestLevel),
        };
        EvtSendEvent?.Invoke($"{FireBaseEvent.level_play}_{PlayerData.Instance.HighestLevel}", _params);
        Debug.LogError("Sent Level");
    }

    private void OnTrackLevelWin()
    {
        Parameter[] _params = new Parameter[]
        {
            new Parameter("level",PlayerData.Instance.HighestLevel),
        };
        EvtSendEvent?.Invoke($"{FireBaseEvent.level_win}_{PlayerData.Instance.HighestLevel}", _params);
        Debug.LogError("SendLevelWin");
    }

    private void OnTrackLevelLose()
    {
        Parameter[] _params = new Parameter[]
        {
            new Parameter("level",PlayerData.Instance.HighestLevel),
        };
        EvtSendEvent?.Invoke($"{FireBaseEvent.level_lose}_{PlayerData.Instance.HighestLevel})", _params);
        Debug.LogError("SendLevelLose");
    }

    private void OnTrackAdInterShow()
    {
        // EvtSendEvent?.Invoke(FireBaseEvent.inter_show,)
        EvtSendEvent?.Invoke($"{FireBaseEvent.inter_show}_{PlayerData.Instance.CountTotalAdInterShown}", null);
        Debug.LogError("ShowInter");
    }

    private void OnTrackAdInterClose()
    {
        EvtSendEvent?.Invoke($"{FireBaseEvent.inter_close}", null);
        Debug.LogError("CloseInter");
    }

    private void OnTrackRewadShow()
    {
        EvtSendEvent?.Invoke($"{FireBaseEvent.reward_show}", null);
        Debug.LogError("ShowReward");
    }

    private void OnTrackRewadComplete()
    {
        EvtSendEvent?.Invoke($"{FireBaseEvent.reward_complete}", null);
        Debug.LogError("RewardComplete");
    }
    #endregion
}

public enum FireBaseEvent
{
    level_play,
    level_win,
    level_lose,
    inter_show,
    inter_close,
    reward_show,
    reward_complete,
}

