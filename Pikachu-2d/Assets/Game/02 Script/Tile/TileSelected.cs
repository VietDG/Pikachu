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
                itemTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                itemTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else
        {
            foreach (var item in GameManager.Instance.itemTileList)
            {
                item.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                if (isSelect && item == itemTile)
                {
                    itemTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
                }
            }
        }
    }
}
