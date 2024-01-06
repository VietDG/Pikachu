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
                if (selectedTile.id == tile.id && selectedTile != tile)//chọn id  = id và k được chọn lại ô mình đã lấy id 
                {

                    Debug.LogError("Match");
                    var match = GameManager.Instance.FindMatch(selectedTile.x, selectedTile.y, tile.x, tile.y);
                    // bắt đầu trận = tạo bảng
                    if (match != null)
                    {
                        TileSelectedEvent?.Invoke(selectedTile, false);
                        TileMatchSucceededEvent?.Invoke(match);

                        GameManager.Instance.Remove(selectedTile.x, selectedTile.y);
                        GameManager.Instance.Remove(tile.x, tile.y);
                        selectedTile = null;
                    }
                    else
                    {
                        TileSelectedEvent?.Invoke(selectedTile, false);
                        TileMatchFailedEvent?.Invoke(selectedTile, tile);
                        selectedTile = null;
                    }
                }
                else
                {
                    TileSelectedEvent?.Invoke(selectedTile, false);
                    selectedTile = null;
                }
            }
            else
            {
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
