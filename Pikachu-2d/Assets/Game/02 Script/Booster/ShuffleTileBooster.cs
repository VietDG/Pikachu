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
            SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("Shuffle"));
            StartCoroutine(shuffleTile.StartShuffleTile(tile.itemTiles, tile.pos));
        }

        TutShuffle();
        return true;
    }

    private void TutShuffle()
    {
        if (PlayerData.Instance.IsShowTutLevel4 == true) return;
        TuttorialManager.Instance._tut4.Close();
    }
}
