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

    public void SetAva(Sprite sprite)
    {
        ava.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TileManager.SelectTile(this);
    }

    public void OnRemoveTile()
    {
        OnRemoveTileEvent?.Invoke();
        OnRemoveTileEvent = null;
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
            _animator.gameObject.transform.localScale = new Vector2(0.67f, 0.67f);
        }
    }

    public void PlayVfx()
    {
        _effect.Play();
        SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("effect_sound"));
        _animator.gameObject.SetActive(false);
    }
}
