using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterShuffle : BoosterView
{
    public TileShuffleEffect tileShuffleEffect;

    public override bool Use()
    {
        if (GamePlayLocker.IsLocked())
            return false;

        var shuffleData = GameManager.Instance.Shuffle();
        if (shuffleData != null)
        {
            StartCoroutine(tileShuffleEffect.PlayEffect(shuffleData.tiles, shuffleData.positions));
        }

        return true;
    }
}
