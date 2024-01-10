using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTile : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer ava;

    public int index;

    public int value;

    public int idTile;

    public void SetTileId(int index)
    {
        this.idTile = index;
    }

    public void SetAva(UnityEngine.Sprite sprite)
    {
        ava.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TileManager.SelectTile(this);
    }

    public void OnRemoveTile()
    {
        EventAction.OnReMoveTile?.Invoke();
        EventAction.OnReMoveTile = null;
    }
}
