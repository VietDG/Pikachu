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
    #endregion

    #region Tile
    public static Action<ItemTile, bool> OnSelectTile;
    public static Action<MatchT> OnMatchTile;
    public static Action<ItemTile, ItemTile> OnMatchTileFail;
    #endregion
    #region Line

    #endregion
}
