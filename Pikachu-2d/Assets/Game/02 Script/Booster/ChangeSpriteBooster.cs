using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteBooster : BoosterView
{
    public ChangeSprite changeSprite;

    public override bool isUseBooster()
    {
        if (MainController.Block())
            return false;

        var tile = GameManager.Instance.GetItemTile();

        changeSprite.StartChangeSprite(tile, GameManager.Instance.GetWidth(), GameManager.Instance.GetHeight());

        return true;
    }
}
