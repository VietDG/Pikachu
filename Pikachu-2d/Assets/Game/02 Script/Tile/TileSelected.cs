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
        foreach (var item in GameManager.Instance.itemTileList)
        {
            item.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            //  item.SetAnim(false);
            if (isSelect && item == itemTile)
            {
                itemTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }
}
