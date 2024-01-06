using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTile : MonoBehaviour, IPointerClickHandler
{
    public int id;

    public int x;

    public int y;

    public SpriteRenderer spriteRenderer;

    public event Action RemovedEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        TileMatchManager.SelectTile(this);
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void OnRemoved()
    {
        RemovedEvent?.Invoke();
        RemovedEvent = null;

        TileRemoveEffect.Instance.ActivateEffectDelayDespawn(transform.localPosition, 1.5f);
    }
}
