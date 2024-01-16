using DG.Tweening;
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

    [SerializeField] Animator _animator;

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
        ScaleTile();
    }

    public void OnRemoveTile()
    {
        EventAction.OnReMoveTile?.Invoke();
        EventAction.OnReMoveTile = null;
    }

    public void ScaleTile()
    {
    }

    public void SetAnim(bool isPlayAnim)
    {
        if (isPlayAnim)
        {
            _animator.enabled = true;
        }
        else
        {
            _animator.enabled = false;
        }
    }
}
