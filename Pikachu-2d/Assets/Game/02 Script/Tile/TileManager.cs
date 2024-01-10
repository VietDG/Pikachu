using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileManager
{
    public static ItemTile itemTile;

    public static void SelectTile(ItemTile tile)
    {
        if (MainController.Block() == false)
        {
            if (itemTile)
            {
                if (itemTile.idTile == tile.idTile && itemTile != tile)
                {
                    var tileMatch = GameManager.Instance.FindTileMatch(itemTile.index, itemTile.value, tile.index, tile.value);
                    if (tileMatch != null)
                    {
                        EventAction.OnSelectTile?.Invoke(itemTile, false);
                        EventAction.OnMatchTile?.Invoke(tileMatch);

                        GameManager.Instance.RemoveTile(itemTile.index, itemTile.value);
                        GameManager.Instance.RemoveTile(tile.index, tile.value);
                        itemTile = null;
                    }
                    else
                    {
                        EventAction.OnSelectTile?.Invoke(itemTile, false);
                        EventAction.OnMatchTileFail?.Invoke(itemTile, tile);//giong nhau ma khong an duoc
                        itemTile = null;
                    }
                }
                else
                {
                    itemTile = null;
                    itemTile = tile;
                    EventAction.OnSelectTile?.Invoke(itemTile, true);
                }
            }
            else
            {
                itemTile = tile;
                EventAction.OnSelectTile?.Invoke(itemTile, true);
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Please wait !!!!!!!!!!");
#endif
        }
    }
}
