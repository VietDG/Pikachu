using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomTile : MonoBehaviour
{
    private float _duration = 5;

    public SpriteRenderer radialFillRenderer;

    [SerializeField] SpriteRenderer _ava;

    [NonSerialized]
    public ItemTile itemTile;

    private float _timeLife;

    private bool isBoom = true;

    public void SpawnBoom()
    {
        _timeLife = _duration;

        isBoom = !itemTile.gameObject.activeSelf;
        _ava.gameObject.SetActive(itemTile.gameObject.activeSelf);

        itemTile.OnRemoveTileEvent += OnRemoveBoom;
    }

    public void DespawnBoom()
    {
        itemTile.OnRemoveTileEvent -= OnRemoveBoom;
    }

    private void OnRemoveBoom()
    {
        this.gameObject.SetActive(false);
        DespawnBoom();
        EventAction.OnRemoveBoom?.Invoke();
    }

    public void Update()
    {
        //if (StateGame.IsPlay())
        //{
        //    Debug.LogError("time");
        if (_timeLife > 0f)
        {
            this.transform.localPosition = itemTile.transform.localPosition;
            if (itemTile.gameObject.activeSelf != isBoom)
            {
                isBoom = itemTile.gameObject.activeSelf;
                _ava.gameObject.SetActive(isBoom);
            }
            _timeLife -= Time.deltaTime;
            if (_timeLife <= 0f)
            {
                StateGame.PauseGame();
                PopupLose.Instance.Show();
            }
        }
    }
    //}
}
