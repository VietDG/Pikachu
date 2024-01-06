using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterFindMatch : BoosterView
{
    public TileMatchEffect tileMatchEffect;

    private SpriteRenderer[] spriteRenders;

    public void Awake()
    {
        TileMatchManager.TileSelectedEvent += OnTileSelected;
    }

    public void OnDestroy()
    {
        TileMatchManager.TileSelectedEvent -= OnTileSelected;
    }

    private void OnTileSelected(ItemTile tile, bool selected)
    {
        if (spriteRenders != null)
        {
            for (int i = 0; i < spriteRenders.Length; i++)
            {
                spriteRenders[i].gameObject.SetActive(false);
            }

            spriteRenders = null;
        }
    }

    public override bool Use()
    {
        Match match = GameManager.Instance.CheckAnyMatch();

        if (match != null)
        {
            tileMatchEffect.CreateHintMatchLine(match, false);
        }

        return true;
    }
}
