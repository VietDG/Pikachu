using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Size
{
    None,
    Right,
    Left,
    Up,
    Down
}

public class SliderTile : MonoBehaviour
{
    private int indexSize = 0;

    private List<Size> sizeList = new List<Size>();

    public void SetSlider(bool top, bool bot, bool lefts, bool rights)
    {
        sizeList.Clear();
        indexSize = 0;

        if (top) sizeList.Add(Size.Up);

        if (bot) sizeList.Add(Size.Down);

        if (lefts) sizeList.Add(Size.Left);

        if (rights) sizeList.Add(Size.Right);
    }

    public IEnumerator StartGetSize()
    {
        if (sizeList.Count > 0 && GameManager.Instance.GetTileCount() > 10)
        {
            indexSize++;
            Size sizes = sizeList[indexSize % sizeList.Count];

            MainController.Augment();

            yield return new WaitForSeconds(0.15f);

            var itemTile = GameManager.Instance.itemTiles;
            int wedth = GameManager.Instance.Width;
            int height = GameManager.Instance.Height;

            List<ItemTile> moveTiles = new List<ItemTile>();
            List<Vector3> positions = new List<Vector3>();

            if (sizes == Size.Down)
            {
                for (int index = 0; index < wedth; index++)
                {
                    for (int t = 1; t < height; t++)
                    {
                        var data = itemTile[index][t];

                        if (data)
                        {
                            int value = t;

                            while (value >= 1 && itemTile[index][value - 1] == null)
                            {
                                value--;
                            }

                            if (value == t) continue;

                            GameManager.Instance.TileMovement(index, value, data);

                            moveTiles.Add(data);
                            positions.Add(GameManager.Instance.GetPosTile(index, value));
                        }
                    }
                }
            }
            else if (sizes == Size.Up)
            {
                for (int index = 0; index < wedth; index++)
                {
                    for (int t = height - 2; t >= 0; t--)
                    {
                        var tile = itemTile[index][t];

                        if (tile)
                        {
                            int value = t;

                            while (value < height - 1 && itemTile[index][value + 1] == null)
                            {
                                value++;
                            }

                            if (value == t) continue;

                            GameManager.Instance.TileMovement(index, value, tile);

                            moveTiles.Add(tile);
                            positions.Add(GameManager.Instance.GetPosTile(index, value));
                        }
                    }
                }
            }
            else if (sizes == Size.Right)
            {
                for (int value = 0; value < height; value++)
                {
                    for (int i = wedth - 2; i >= 0; i--)
                    {
                        var tile = itemTile[i][value];

                        if (tile)
                        {
                            int index = i;

                            while (index < wedth - 1 && itemTile[index + 1][value] == null)
                            {
                                index++;
                            }
                            GameManager.Instance.TileMovement(index, value, tile);

                            moveTiles.Add(tile);
                            positions.Add(GameManager.Instance.GetPosTile(index, value));
                        }
                    }
                }
            }
            else if (sizes == Size.Left)
            {
                for (int value = 0; value < height; value++)
                {
                    for (int t = 1; t < wedth; t++)
                    {
                        var data = itemTile[t][value];

                        if (data)
                        {
                            int index = t;

                            while (index >= 1 && itemTile[index - 1][value] == null)
                            {
                                index--;
                            }
                            GameManager.Instance.TileMovement(index, value, data);

                            moveTiles.Add(data);
                            positions.Add(GameManager.Instance.GetPosTile(index, value));
                        }
                    }
                }
            }

            for (int i = 0; i < moveTiles.Count; i++)
            {
                moveTiles[i].transform.DOMove(positions[i], 0.25f);
            }

            yield return new WaitForSeconds(0.25f);

            MainController.SetAllTileSize();

#if UNITY_EDITOR
            var vec = new List<Vector2Int>();

            for (int value = 0; value < height; value++)
            {
                for (int index = 0; index < wedth; index++)
                {
                    var tile = itemTile[index][value];

                    if (tile)
                    {
                        if (tile.index != index || tile.value != value)
                        {
                        }

                        if (vec.Contains(new Vector2Int(tile.index, tile.value)))
                        {
                        }
                        else
                            vec.Add(new Vector2Int(tile.index, tile.value));
                    }
                }
            }
#endif
        }
    }
}
