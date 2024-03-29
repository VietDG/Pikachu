﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelectedEffect : MonoBehaviour
{
    private void Awake()
    {
        TileMatchManager.TileSelectedEvent += OnTileSelected;
    }

    private void OnDestroy()
    {
        TileMatchManager.TileSelectedEvent -= OnTileSelected;
    }

    private void OnTileSelected(ItemTile tile, bool selected)
    {
        if (selected)
        {
            tile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            tile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

}
