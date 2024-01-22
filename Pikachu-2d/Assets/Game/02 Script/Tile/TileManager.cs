using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileManager
{
    public static ItemTile itemTile;

    public static void SelectTile(ItemTile tiles)
    {
        if (MainController.Block() == false)
        {
            if (itemTile)
            {
                if (itemTile.idTile == tiles.idTile && itemTile != tiles)
                {
                    var tileMatch = GameManager.Instance.FindTileMatch(itemTile.index, itemTile.value, tiles.index, tiles.value);
                    if (tileMatch != null)
                    {
                        EventAction.OnSelectTile?.Invoke(itemTile, false);
                        EventAction.OnMatchTile?.Invoke(tileMatch);

                        GameManager.Instance.RemoveTile(itemTile.index, itemTile.value);
                        GameManager.Instance.RemoveTile(tiles.index, tiles.value);
                        itemTile = null;
                        SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("effect_sound"));
                    }
                    else
                    {
                        EventAction.OnSelectTile?.Invoke(itemTile, false);
                        EventAction.OnMatchTileFail?.Invoke(itemTile, tiles);//giong nhau ma khong an duoc
                        itemTile = null;
                    }
                }
                else
                {
                    itemTile = null;
                    itemTile = tiles;
                    EventAction.OnSelectTile?.Invoke(itemTile, true);
                }
            }
            else
            {
                itemTile = tiles;
                EventAction.OnSelectTile?.Invoke(itemTile, true);
            }
        }
    }
}
