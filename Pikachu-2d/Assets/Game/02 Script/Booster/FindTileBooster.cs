using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTileBooster : BoosterController
{
    public MatchTile matchTile;

    private SpriteRenderer[] ava;

    public void Awake()
    {
        EventAction.OnSelectTile += OnTileSelect;
    }

    public void OnDestroy()
    {
        EventAction.OnSelectTile -= OnTileSelect;
    }

    private void OnTileSelect(ItemTile itemTile, bool isSelect)
    {
        if (ava != null)
        {
            for (int i = 0; i < ava.Length; i++)
            {
                ava[i].gameObject.SetActive(false);
            }

            ava = null;
        }
    }

    public override bool isUseBooster()
    {
        MatchT matchTile = GameManager.Instance.FindAllTile();

        if (matchTile != null)
        {
            this.matchTile.CreateMatchLine(matchTile, false);
            this.matchTile.CreatDot(matchTile);
        }

        return true;
    }

    public void TutChangeSprite()
    {
        if (PlayerData.Instance.IsShowTutLevel5 == true) return;
        TuttorialManager.Instance._tut5.Close();
    }
}
