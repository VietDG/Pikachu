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

    private void OnSelectTile(ItemTile tile, bool selected)
    {
        foreach (var item in GameManager.Instance.itemTileList)
        {
            item.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            if (selected && item == tile)
            {
                tile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

}
