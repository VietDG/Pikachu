using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileMatchManager
{
    public static ItemTile selectedTile;

    public static event Action<ItemTile, bool> TileSelectedEvent;

    public static event Action<Match> TileMatchSucceededEvent;

    public static event Action<ItemTile, ItemTile> TileMatchFailedEvent;

    public static void SelectTile(ItemTile tile)
    {
        if (GamePlayLocker.IsLocked() == false)
        {
            if (selectedTile)// lựa chọn 
            {
                Debug.LogError("d");
                if (selectedTile.id == tile.id && selectedTile != tile)//chọn id  = id và k được chọn lại ô mình đã lấy id 
                {
                    Debug.LogError("true");
                    var match = GameManager.Instance.FindMatch(selectedTile.x, selectedTile.y, tile.x, tile.y);
                    // bắt đầu trận = tạo bảng
                    if (match != null)
                    {
                        TileSelectedEvent?.Invoke(selectedTile, false);
                        TileMatchSucceededEvent?.Invoke(match);

                        GameManager.Instance.Remove(selectedTile.x, selectedTile.y);
                        GameManager.Instance.Remove(tile.x, tile.y);
                        selectedTile = null;
                        Debug.LogError("an");
                    }
                    else
                    {
                        TileSelectedEvent?.Invoke(selectedTile, false);
                        TileMatchFailedEvent?.Invoke(selectedTile, tile);//giong nhau ma khong an duoc
                        selectedTile = null;
                    }
                }
                else
                {
                    Debug.LogError("false");
                    //TileSelectedEvent?.Invoke(selectedTile, false);
                    selectedTile = null;
                    selectedTile = tile;
                    TileSelectedEvent?.Invoke(selectedTile, true);
                }
            }
            else
            {
                Debug.LogError("c");
                selectedTile = tile;
                TileSelectedEvent?.Invoke(selectedTile, true);
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
