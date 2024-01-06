using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float duration = 5;

    public SpriteRenderer radialFillRenderer;

    public SpriteRenderer bombSpriteRenderer;

    [NonSerialized]
    public ItemTile tile;

    private float time;

    private Material radialFillMaterial;

    private static int radialAngleId = Shader.PropertyToID("_Arc2");

    private bool isTileActive = true;

    public Action RemovedEvent;

    public void OnSpawn()
    {
        time = duration;
        radialFillMaterial = radialFillRenderer.material;

        isTileActive = !tile.gameObject.activeSelf;
        radialFillRenderer.gameObject.SetActive(tile.gameObject.activeSelf);
        bombSpriteRenderer.gameObject.SetActive(tile.gameObject.activeSelf);

        tile.RemovedEvent += OnTileRemoved;
    }

    public void OnDespawn()
    {
        tile.RemovedEvent -= OnTileRemoved;
    }

    private void OnTileRemoved()
    {
        gameObject.SetActive(false);
        OnDespawn();

        RemovedEvent?.Invoke();
    }

    public void Update()
    {
        if (time > 0f)
        {
            transform.localPosition = tile.transform.localPosition;

            if (tile.gameObject.activeSelf != isTileActive)
            {
                isTileActive = tile.gameObject.activeSelf;
                radialFillRenderer.gameObject.SetActive(isTileActive);
                bombSpriteRenderer.gameObject.SetActive(isTileActive);
            }

            time -= Time.deltaTime;
            radialFillMaterial.SetFloat(radialAngleId, Mathf.Max(0f, time / duration * 360f));

            if (time <= 0f)
            {
                GamePlayState.Pause();

            }
        }
    }
}
