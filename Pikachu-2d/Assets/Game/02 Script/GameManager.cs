using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataShuf
{
    public ItemTile[] itemTiles;
    public Vector3[] pos;
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [Header("--------------Reference-----------------")]
    public ItemTile tilePrefab;

    public ItemTile[][] itemTiles;

    public float space = 0.9f;

    [SerializeField] Transform mainTrans;

    [Header("--------------Value---------------")]
    public List<ItemTile> itemTileList = new List<ItemTile>();

    private Vector2 originalPos;

    private TileData tileData;

    private Dictionary<int, List<ItemTile>> tileDict = new Dictionary<int, List<ItemTile>>();

    public int Width { get; set; }

    public int Height { get; set; }

    public Dictionary<int, List<ItemTile>> GetTileDict()
    {
        return tileDict;
    }

    private void OnDestroy()
    {
        tileData = null;
    }

    public void OnSpawnTile(TileData tileData)
    {
        if (this.tileData != tileData || Width != tileData.widths || Height != tileData.heights || itemTileList.Count != this.tileData.GetAmoutTile())
        {
            this.tileData = tileData;
            Width = this.tileData.widths;
            Height = this.tileData.heights;

            itemTiles = new ItemTile[Width][];
            for (int x = 0; x < Width; x++)
            {
                itemTiles[x] = new ItemTile[Height];
            }
            int boardTileCount = this.tileData.GetAmoutTile();

            if (boardTileCount > itemTileList.Count)
            {
                for (int i = boardTileCount - itemTileList.Count - 1; i >= 0; i--)
                {
                    ItemTile tile = Instantiate(tilePrefab, mainTrans);
                    itemTileList.Add(tile);
                }
            }
            else
            {
                itemTileList.RemoveRange(boardTileCount, itemTileList.Count - boardTileCount);
            }
        }
        else
        {
            Width = this.tileData.widths;
            Height = this.tileData.heights;
        }

        TileSpriteList tileSpritePack = TileSpriteListManager.Instance.GetTileSpriteList();
        int index = 0;
        originalPos = new Vector2(-Width * space * 0.5f, -Height * space * 0.5f);
        tileDict.Clear();

        do
        {
            if (this.tileData.isShuffle)
                this.tileData.ShufflePos();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    itemTiles[x][y] = null;

                    int tileCode = this.tileData.dataTile[x][y];
                    if (tileCode != TileData.tileCode)
                    {
                        ItemTile tile = itemTileList[index++];
                        tile.gameObject.SetActive(true);
                        tile.SetTileId(tileCode);
                        tile.SetAva(tileSpritePack.GetSprite(tileCode));
                        tile.index = x;
                        tile.value = y;
                        tile.transform.position = GetPosTile(x, y);
                        itemTiles[x][y] = tile;

                        if (tileDict.ContainsKey(tileCode))
                        {
                            tileDict[tileCode].Add(tile);
                        }
                        else
                        {
                            var tiles = new List<ItemTile>();
                            tiles.Add(tile);
                            tileDict[tileCode] = tiles;
                        }
                    }
                }
            }
        }
        while (FindAllTile() == null && this.tileData.isShuffle);
    }

    public ItemTile[][] GetItemTile()
    {
        return itemTiles;
    }

    public void TileMovement(int x1, int y1, ItemTile itemTile)
    {
        if (itemTile == itemTiles[itemTile.index][itemTile.value])
        {
            itemTiles[itemTile.index][itemTile.value] = null;
            tileData.SetData(itemTile.index, itemTile.value, TileData.tileCode);

            itemTile.index = x1;
            itemTile.value = y1;
            itemTiles[x1][y1] = itemTile;
            tileData.SetData(x1, y1, itemTile.idTile);
        }
    }

    public int GetWidth()
    {
        return Width;
    }

    public int GetHeight()
    {
        return Height;
    }

    public int GetTileCount()
    {
        return tileData.GetAmoutTile();
    }

    public DataShuf Shuffle()
    {
        if (MainController.Block() || GetTileCount() < 2)
            return null;

        List<ItemTile> itemTile = new List<ItemTile>();

        for (int h = 0; h < Height; h++)
        {
            for (int w = 0; w < Width; w++)
            {
                ItemTile data = itemTiles[w][h];

                if (data)
                {
                    tileData.SetData(w, h, TileData.tileCode);
                    itemTile.Add(data);
                }
            }
        }

        List<ItemTile> itemTileShuffle;
        Vector2Int[] newPos;

        int count = 0;

        while (true)
        {
            itemTileShuffle = new List<ItemTile>(itemTile);
            itemTileShuffle.Shuffle();

            newPos = new Vector2Int[itemTileShuffle.Count];

            for (int i = 0; i < itemTileShuffle.Count; i++)
            {
                ItemTile targetTile = itemTileShuffle[i];
                tileData.SetData(targetTile.index, targetTile.value, itemTile[i].idTile);
                itemTiles[targetTile.index][targetTile.value] = itemTile[i];

                newPos[i] = new Vector2Int(targetTile.index, targetTile.value);
            }

            for (int i = 0; i < itemTile.Count; i++)
            {
                ItemTile data = itemTile[i];
                data.index = newPos[i].x;
                data.value = newPos[i].y;
            }

            var match = FindAllTile();
            if (match != null)
            {
                break;
            }
            else
            {
                if (count == 1000)
                {
                    break;
                }
                count++;
            }
        }

        Vector3[] targetPos = new Vector3[itemTile.Count];
        for (int i = 0; i < itemTile.Count; i++)
        {
            ItemTile data = itemTile[i];
            targetPos[i] = GetPosTile(data.index, data.value);
        }

        return new DataShuf()
        {
            itemTiles = itemTile.ToArray(),
            pos = targetPos
        };
    }

    public MatchT FindAllTile()
    {
        ItemTile t1 = null;
        ItemTile t2 = null;

        foreach (var list in tileDict.Values)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                t1 = list[i];

                for (int j = i + 1; j < list.Count; j++)
                {
                    t2 = list[j];

                    var matchTile = FindTileMatch(t1.index, t1.value, t2.index, t2.value);

                    if (matchTile != null)
                    {
                        return matchTile;
                    }
                }
            }
        }

        return null;
    }

    // lay kich thuoc man hinh
    public Vector2 GetSizeTile()
    {
        return new Vector2(tileData.widths * space, tileData.heights * space);
    }

    //lay vi tri cua tile
    public Vector3 GetPosTile(int index, int value)
    {
        return new Vector3((index + 0.5f) * space + originalPos.x, (value + 0.5f) * space + originalPos.y, 0f);
    }

    //xoa tile
    public void RemoveTile(int index, int value)
    {
        ItemTile itemTile = itemTiles[index][value];

        if (itemTile)
        {
            if (tileDict.ContainsKey(itemTile.idTile))
            {
                var list = tileDict[itemTile.idTile];
                list.Remove(itemTile);

                if (list.Count < 2)
                {
                    tileDict.Remove(itemTile.idTile);
                }
            }

            itemTile.gameObject.SetActive(false);
            itemTile.OnRemoveTile();
        }

        itemTiles[index][value] = null;
        tileData.RemoveTile(index, value);

        if (tileData.GetAmoutTile() == 0)
        {
            EventAction.WinGame?.Invoke();
        }
    }

    public MatchT FindTileMatch(int x1, int y1, int x2, int y2)
    {
        return tileData.Find(x1, y1, x2, y2);
    }
}
