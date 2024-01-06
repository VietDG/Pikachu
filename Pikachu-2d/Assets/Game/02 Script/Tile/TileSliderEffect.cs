using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSliderEffect : MonoBehaviour
{
    public enum Side
    {
        None, Right, Left, Up, Down
    }

    //   public AnimationCurve animCurve = AnimationCurve.Linear(0f, 1f, 0f, 1f);

    public float duration = 0.5f;

    private int sideIndex = 0;

    private List<Side> sideCache = new List<Side>();

    public void SetSlideOrder(bool up, bool down, bool left, bool right)
    {
        sideCache.Clear();
        sideIndex = 0;

        if (up) sideCache.Add(Side.Up);

        if (down) sideCache.Add(Side.Down);

        if (left) sideCache.Add(Side.Left);

        if (right) sideCache.Add(Side.Right);
    }

    public IEnumerator PlayCoroutine()
    {
        if (sideCache.Count > 0 && GameManager.Instance.GetTileCount() > 10)
        {
            sideIndex++;
            Side side = sideCache[sideIndex % sideCache.Count];

            GamePlayLocker.Retain();

            yield return new WaitForSeconds(0.15f);

            var tiles = GameManager.Instance.tiles;
            int width = GameManager.Instance.width;
            int height = GameManager.Instance.height;

            List<ItemTile> moveTiles = new List<ItemTile>();
            List<Vector3> positions = new List<Vector3>();

            if (side == Side.Down)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 1; y < height; y++)
                    {
                        var tile = tiles[x][y];

                        if (tile)
                        {
                            int dy = y;

                            while (dy >= 1 && tiles[x][dy - 1] == null)
                            {
                                dy--;
                            }

                            if (dy == y) continue;

                            GameManager.Instance.MoveTile(x, dy, tile);

                            moveTiles.Add(tile);
                            positions.Add(GameManager.Instance.GetPosition(x, dy));
                        }
                    }
                }
            }
            else if (side == Side.Up)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = height - 2; y >= 0; y--)
                    {
                        var tile = tiles[x][y];

                        if (tile)
                        {
                            int dy = y;

                            while (dy < height - 1 && tiles[x][dy + 1] == null)
                            {
                                dy++;
                            }

                            if (dy == y) continue;

                            GameManager.Instance.MoveTile(x, dy, tile);

                            moveTiles.Add(tile);
                            positions.Add(GameManager.Instance.GetPosition(x, dy));
                        }
                    }
                }
            }
            else if (side == Side.Right)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = width - 2; x >= 0; x--)
                    {
                        var tile = tiles[x][y];

                        if (tile)
                        {
                            int dx = x;

                            while (dx < width - 1 && tiles[dx + 1][y] == null)
                            {
                                dx++;
                            }
                            GameManager.Instance.MoveTile(dx, y, tile);

                            moveTiles.Add(tile);
                            positions.Add(GameManager.Instance.GetPosition(dx, y));
                        }
                    }
                }
            }
            else if (side == Side.Left)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 1; x < width; x++)
                    {
                        var tile = tiles[x][y];

                        if (tile)
                        {
                            int dx = x;

                            while (dx >= 1 && tiles[dx - 1][y] == null)
                            {
                                dx--;
                            }
                            GameManager.Instance.MoveTile(dx, y, tile);

                            moveTiles.Add(tile);
                            positions.Add(GameManager.Instance.GetPosition(dx, y));
                        }
                    }
                }
            }

            for (int i = 0; i < moveTiles.Count; i++)
            {
                moveTiles[i].transform.DOMove(positions[i], 0.25f);
            }

            yield return new WaitForSeconds(0.25f);

            GamePlayLocker.Release();

#if UNITY_EDITOR
            var b = new List<Vector2Int>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var tile = tiles[x][y];

                    if (tile)
                    {
                        if (tile.x != x || tile.y != y)
                        {
                            Debug.LogError(tile.x + "-" + tile.y);
                        }

                        if (b.Contains(new Vector2Int(tile.x, tile.y)))
                        {
                            Debug.Log("Duplicate : " + tile.x + "-" + tile.y);
                        }
                        else
                            b.Add(new Vector2Int(tile.x, tile.y));
                    }
                }
            }
#endif
        }
    }
}
