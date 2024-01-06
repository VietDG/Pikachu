using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterChangePicture : BoosterView
{
    public TileSwapTextureEffect tileSwapEffect;

    public override bool Use()
    {
        if (GamePlayLocker.IsLocked())
            return false;

        var gameBoard = GameManager.Instance;
        var tiles = gameBoard.GetTiles();

        tileSwapEffect.PlayEffect(tiles, gameBoard.GetWidth(), gameBoard.GetHeight());

        return true;
    }
}
