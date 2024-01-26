using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelected : MonoBehaviour
{
    private void Awake()
    {
        EventAction.OnSelectTile += OnSelectTile;
    }

    private void OnDestroy()
    {
        EventAction.OnSelectTile -= OnSelectTile;
    }

    private void OnSelectTile(ItemTile itemTile, bool isSelect)
    {
        if (PlayerData.Instance.HighestLevel <= 1)
        {
            if (itemTile)
            {
                itemTile.SetLayerYellow();
            }
            else
            {
                itemTile.SetLayerWhite();
            }
        }
        else
        {
            foreach (var item in GameManager.Instance.itemTileList)
            {
                if (item.isAnim == true)
                {
                    item.SetLayerGreen();
                }
                else
                {
                    item.SetLayerWhite();
                }
                if (isSelect && item == itemTile)
                {
                    item.SetLayerYellow();
                }
            }
        }
    }
}
