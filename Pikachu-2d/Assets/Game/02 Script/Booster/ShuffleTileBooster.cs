using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleTileBooster : BoosterController
{
    public ShuffleTile shuffleTile;

    public override bool isUseBooster()
    {
        if (MainController.Block())
            return false;

        var tile = GameManager.Instance.Shuffle();
        if (tile != null)
        {
            StartCoroutine(shuffleTile.StartShuffleTile(tile.itemTiles, tile.pos));
        }

        return true;
    }
}
