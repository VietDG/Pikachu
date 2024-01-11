using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : Singleton<TileData>
{
    public int[][] dataTile;

    public const int tileCode = -1;

    public int widths = 6;

    public int heights = 19;

    public bool isShuffle = true;

    private int type = 10;

    private int amout;

    private List<Vector2Int> newPos = new List<Vector2Int>();

    public void Initialize(LevelConfig loadlevelFormData, BoardConfig loadBoardFormData, int maxValue)
    {
        newPos.Clear();
        amout = 0;
        widths = Mathf.Max(2, loadBoardFormData.row);
        heights = Mathf.Max(2, loadBoardFormData.col);
        type = loadlevelFormData.kind;
        isShuffle = !loadBoardFormData.containTileIndex;

        dataTile = new int[widths][];
        for (int i = 0; i < widths; i++)
        {
            dataTile[i] = new int[heights];
        }

        if (isShuffle)
        {
            for (int x = 0; x < widths; x++)
            {
                for (int y = 0; y < heights; y++)
                {
                    int index = (heights - y - 1) * widths + x;
                    if (loadBoardFormData.datas[index] == 0)
                    {
                        newPos.Add(new Vector2Int(x, y));

                        amout++;
                    }
                    else
                    {
                        dataTile[x][y] = tileCode;
                    }
                }
            }
        }
        else
        {
            for (int x = 0; x < widths; x++)
            {
                for (int y = 0; y < heights; y++)
                {
                    int index = (heights - y - 1) * widths + x;
                    if (loadBoardFormData.datas[index] >= 0)
                    {
                        dataTile[x][y] = loadBoardFormData.datas[index];

                        amout++;
                    }
                    else
                    {
                        dataTile[x][y] = tileCode;
                    }
                }
            }
        }
    }

    public void ShufflePos()
    {
        newPos.Shuffle();

        List<int> type = new List<int>();
        for (int i = 0; i < this.type; i++)
        {
            type.Add(i);
        }
        type.Shuffle();

        for (int i = 0; i < newPos.Count; i += 2)
        {
            int typeIndex = (i / 2) % this.type;

            Vector2Int l1 = newPos[i];
            Vector2Int l2 = newPos[i + 1];

            dataTile[l1.x][l1.y] = type[typeIndex];
            dataTile[l2.x][l2.y] = type[typeIndex];
        }
    }

    public int GetAmoutTile()
    {
        return amout;
    }

    public void RemoveTile(int x, int y)
    {
        dataTile[x][y] = tileCode;
        amout--;
    }

    public void SetData(int x, int y, int id)
    {
        dataTile[x][y] = id;
    }

    public MatchT Find(int x1, int y1, int x2, int y2)
    {
        MatchT matchTile = null;
        if (x1 == x2 || y1 == y2)
        {
            if (x1 == x2 && IsRemoveLineV(x1, y1, y2))
            {
                matchTile = new MatchT();
                matchTile.AddPos(x1, y1);
                matchTile.AddPos(x2, y2);

                return matchTile;
            }
            else if (y1 == y2 && IsRemoveLineH(x1, x2, y1))
            {
                matchTile = new MatchT();
                matchTile.AddPos(x1, y1);
                matchTile.AddPos(x2, y2);
                return matchTile;
            }
        }

        if (FindVertical(x1, y1, x2, y2))
        {
            matchTile = new MatchT();
            matchTile.AddPos(x1, y1);
            matchTile.AddPos(x1, y2);
            matchTile.AddPos(x2, y2);

            return matchTile;
        }
        else if (FindHorizontal(x1, y1, x2, y2))
        {
            matchTile = new MatchT();
            matchTile.AddPos(x1, y1);
            matchTile.AddPos(x2, y1);
            matchTile.AddPos(x2, y2);

            return matchTile;
        }

        for (int x = x1 - 1; x >= -1; x--)
        {
            if (IsNone(x, y1))
            {
                if (FindVertical(x, y1, x2, y2))
                {
                    matchTile = new MatchT();
                    matchTile.AddPos(x1, y1);
                    matchTile.AddPos(x, y1);
                    matchTile.AddPos(x, y2);
                    matchTile.AddPos(x2, y2);

                    return matchTile;
                }
            }
            else
            {
                break;
            }
        }
        for (int x = x1 + 1; x <= widths; x++)
        {
            if (IsNone(x, y1))
            {
                if (FindVertical(x, y1, x2, y2))
                {
                    matchTile = new MatchT();
                    matchTile.AddPos(x1, y1);
                    matchTile.AddPos(x, y1);
                    matchTile.AddPos(x, y2);
                    matchTile.AddPos(x2, y2);

                    return matchTile;
                }
            }
            else
            {
                break;
            }
        }
        for (int y = y1 - 1; y >= -1; y--)
        {
            if (IsNone(x1, y))
            {
                if (FindHorizontal(x1, y, x2, y2))
                {
                    matchTile = new MatchT();
                    matchTile.AddPos(x1, y1);
                    matchTile.AddPos(x1, y);
                    matchTile.AddPos(x2, y);
                    matchTile.AddPos(x2, y2);
                    return matchTile;
                }
            }
            else
            {
                break;
            }
        }
        for (int y = y1 + 1; y <= heights; y++)
        {
            if (IsNone(x1, y))
            {
                if (FindHorizontal(x1, y, x2, y2))
                {
                    matchTile = new MatchT();
                    matchTile.AddPos(x1, y1);
                    matchTile.AddPos(x1, y);
                    matchTile.AddPos(x2, y);
                    matchTile.AddPos(x2, y2);

                    return matchTile;
                }
            }
            else
            {
                break;
            }
        }

        return null;
    }

    private bool FindVertical(int x1, int y1, int x2, int y2)
    {
        if (IsNone(x1, y2) && IsRemoveLineV(x1, y1, y2) && IsRemoveLineH(x1, x2, y2))
        {
            return true;
        }

        return false;
    }

    private bool FindHorizontal(int x1, int y1, int x2, int y2)
    {
        if (IsNone(x2, y1) && IsRemoveLineV(x2, y1, y2) && IsRemoveLineH(x1, x2, y1))
        {
            return true;
        }

        return false;
    }

    private bool IsRemoveLineH(int x1, int x2, int y)
    {
        int index1 = x1;
        int index2 = x2;

        if (x2 < x1)
        {
            index1 = x2;
            index2 = x1;
        }

        for (int x = index1 + 1; x < index2; x++)
        {
            if (!IsNone(x, y))
                return false;
        }

        return true;
    }

    private bool IsRemoveLineV(int x, int y1, int y2)
    {
        int value1 = y1;
        int value2 = y2;

        if (y2 < y1)
        {
            value1 = y2;
            value2 = y1;
        }

        for (int y = value1 + 1; y < value2; y++)
        {
            if (!IsNone(x, y))
                return false;
        }

        return true;
    }

    private bool IsNone(int x, int y)
    {
        return x < 0 || x >= widths || y < 0 || y >= heights || dataTile[x][y] == tileCode;
    }
}

public class MatchT
{
    public void AddPos(int x, int y)
    {
        posList.Add(new Vector2Int(x, y));
    }

    public List<Vector2Int> posList = new List<Vector2Int>();
}

public static class IList
{
    public static void Shuffle<T>(this IList<T> list)
    {
        var max = list.Count;
        var min = max - 1;
        for (var i = 0; i < min; ++i)
        {
            var a = UnityEngine.Random.Range(i, max);
            var tmp = list[i];
            list[i] = list[a];
            list[a] = tmp;
        }
    }
}
