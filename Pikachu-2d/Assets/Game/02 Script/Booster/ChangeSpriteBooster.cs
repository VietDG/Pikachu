using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteBooster : BoosterController
{
    public ChangeSprite changeSprite;

    public override bool isUseBooster()
    {
        if (MainController.Block())
            return false;

        var tile = GameManager.Instance.GetItemTile();

        SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("ChangeImg"));
        changeSprite.StartChangeSprite(tile, GameManager.Instance.GetWidth(), GameManager.Instance.GetHeight());
        TutChangeSprite();

        return true;
    }

    public void TutChangeSprite()
    {
        if (PlayerData.Instance.IsShowTutLevel3 == true) return;
        TuttorialManager.Instance._tut3.Close();
    }
}
