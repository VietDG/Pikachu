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

    public SpriteRenderer _layer;

    [SerializeField] Sprite _layerWhite, _layerGreen, _layerYellow;

    public int idTile;

    public int index;

    public int value;

    public Animator _animator;

    public bool isAnim;

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
            SetLayerGreen();
            isAnim = true;
        }
        else
        {
            _animator.enabled = false;
            _animator.gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
            SetLayerWhite();
            isAnim = false;
        }
    }

    public void SetLayerWhite()
    {
        _layer.sprite = _layerWhite;
    }

    public void SetLayerGreen()
    {
        _layer.sprite = _layerGreen;
    }

    public void SetLayerYellow()
    {
        _layer.sprite = _layerYellow;
    }

    public void PlayVfx()
    {
        _effect.Play();
        SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("effect_sound"));
        _animator.gameObject.SetActive(false);
    }
}
