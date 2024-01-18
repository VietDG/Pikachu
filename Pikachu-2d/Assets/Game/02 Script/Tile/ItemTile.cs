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

    public Animator _animator;

    [SerializeField] ParticleSystem _effect;

    public Vector2 Lerp;

    public event Action OnRemoveTileEvent;

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
        //EventAction.OnReMoveTile?.Invoke();
        //EventAction.OnReMoveTile = null;

        OnRemoveTileEvent?.Invoke();
        OnRemoveTileEvent = null;
    }

    public void ScaleTile()
    {
        //this.transform.DOScale(Lerp, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
        //{
        //    this.transform.localScale = Vector2.one;
        //});
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

    public void PlayVfx()
    {
        _effect.Play();
        _animator.gameObject.SetActive(false);
    }
}
