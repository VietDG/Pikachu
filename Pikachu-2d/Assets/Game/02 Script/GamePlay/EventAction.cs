using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAction
{
    #region Tile

    #endregion

    #region GamePlay

    public static Action WinGame;
    public static Action OnNextLevel;

    public static Action OnRevive;
    #endregion

    #region Tile
    public static Action<ItemTile, bool> OnSelectTile;
    public static Action<MatchT> OnMatchTile;
    public static Action<ItemTile, ItemTile> OnMatchTileFail;

    public static Action OnRemoveBoom;
    #endregion
    #region Line

    #endregion

    #region Ads
    public static Action OnShowBanner;
    public static Action OnHideBanner;

    public static Action EventTrackLevelPlay;
    public static Action EventTrackLevelWin;
    public static Action EventTrackLoseLevel;
    public static Action EventtTotalAdInterShown;
    public static Action EventInterClose;
    public static Action EventRewardShow;
    public static Action EventRewardComplete;
    #endregion
}
