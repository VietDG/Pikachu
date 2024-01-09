using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public class ShuffleData         // trộn dữ liệu 
    {
        public ItemTile[] tiles;    // gạch
        public Vector3[] positions;
    }

    public ItemTile tilePrefab;

    public ItemTile[][] tiles;

    public float spacing = 0.9f;

    public int width { get; set; }

    public int height { get; set; }

    private Vector2 originPosition;

    private TileData boardTileData;

    private Dictionary<int, List<ItemTile>> tileGroups = new Dictionary<int, List<ItemTile>>();

    public List<ItemTile> tileList = new List<ItemTile>();

    private List<BombController> bombList = new List<BombController>();

    public event Action OnBoardClear;

    [SerializeField] Transform mainTrans;

    public Dictionary<int, List<ItemTile>> GetTileGroups()
    {
        return tileGroups;
    }

    private void OnDestroy()
    {
        boardTileData = null;
    }

    public void SpawnTiles(TileData boardData)
    {
        if (boardTileData != boardData || width != boardData.width || height != boardData.height || tileList.Count != boardTileData.GetTileCount())
        {
            boardTileData = boardData;
            width = boardTileData.width;
            height = boardTileData.height;//

            tiles = new ItemTile[width][];// tạo ra bảng có chiều rộng, chưa có chiều cao 
            for (int x = 0; x < width; x++)
            {
                tiles[x] = new ItemTile[height];// tạo chiều cao cho bảng 
            }

            int boardTileCount = boardTileData.GetTileCount();// tạo bảng nhập số lượng ô

            if (boardTileCount > tileList.Count)
            {
                for (int i = boardTileCount - tileList.Count - 1; i >= 0; i--)
                {
                    ItemTile tile = Instantiate(tilePrefab, mainTrans);

                    tileList.Add(tile);// thêm ô
                }
            }
            else
            {
                tileList.RemoveRange(boardTileCount, tileList.Count - boardTileCount);// xóa phạm vi
            }
        }
        else
        {
            width = boardTileData.width;
            height = boardTileData.height;
        }

        TileSpritePack tileSpritePack = TileSpritePackManager.Instance.GetTileSpritePack();// lấy dữ liệu data map chơi 
        int index = 0;
        originPosition = new Vector2(-width * spacing * 0.5f, -height * spacing * 0.5f);// khoảng cách 
        tileGroups.Clear();// xóa list

        do
        {
            if (boardTileData.canShuffleOnInit) // nếu xáo bảng 
                boardTileData.ShuffleLocation();// tạo bảng vị trí ngẫu nhiên

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x][y] = null;

                    int tileCode = boardTileData.data[x][y];
                    if (tileCode != TileData.EmptyTileCode)
                    {
                        ItemTile tile = tileList[index++];
                        tile.gameObject.SetActive(true);
                        tile.SetId(tileCode);
                        tile.SetSprite(tileSpritePack.Get(tileCode));
                        tile.x = x;
                        tile.y = y;
                        tile.transform.position = GetPosition(x, y);
                        tiles[x][y] = tile;

                        if (tileGroups.ContainsKey(tileCode))
                        {
                            tileGroups[tileCode].Add(tile);
                        }
                        else
                        {
                            var tiles = new List<ItemTile>();
                            tiles.Add(tile);
                            tileGroups[tileCode] = tiles;
                        }
                    }
                }
            }

            if (index != boardTileData.GetTileCount())
            {
                Debug.LogError("Index and board tile count is not match.Something is wrong!");
            }
        }
        while (CheckAnyMatch() == null && boardTileData.canShuffleOnInit);
    }

    public ItemTile[][] GetTiles()
    {
        return tiles;
    }

    public void MoveTile(int xt, int yt, ItemTile tile)// di chuyển ô gạch khi cần gợi ý 
    {
        if (tile == tiles[tile.x][tile.y])
        {
            tiles[tile.x][tile.y] = null;
            boardTileData.SetTileData(tile.x, tile.y, TileData.EmptyTileCode);

            tile.x = xt;
            tile.y = yt;
            tiles[xt][yt] = tile;
            boardTileData.SetTileData(xt, yt, tile.id);
        }
        else
        {
            Debug.Log("Can not move tile");
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetTileCount()// số lượng ô gạch 
    {
        return boardTileData.GetTileCount();
    }

    public ShuffleData Shuffle() // trộn ô gach
    {

        if (GamePlayLocker.IsLocked() || GetTileCount() < 2)
            return null;

        //List<Vector2Int> b = new List<Vector2Int>();

        List<ItemTile> currentTiles = new List<ItemTile>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ItemTile tile = tiles[x][y];

                if (tile)
                {
                    boardTileData.SetTileData(x, y, TileData.EmptyTileCode);
                    currentTiles.Add(tile);
                }
            }
        }

        List<ItemTile> shuffledCurrentTiles;
        Vector2Int[] shuffledLocations;

        int shuffleCount = 0;

        while (true)
        {
            shuffledCurrentTiles = new List<ItemTile>(currentTiles);
            shuffledCurrentTiles.Shuffle();// thuật toán random vị trí 

            shuffledLocations = new Vector2Int[shuffledCurrentTiles.Count];

            for (int i = 0; i < shuffledCurrentTiles.Count; i++)
            {
                ItemTile targetTile = shuffledCurrentTiles[i];
                boardTileData.SetTileData(targetTile.x, targetTile.y, currentTiles[i].id);
                tiles[targetTile.x][targetTile.y] = currentTiles[i];

                shuffledLocations[i] = new Vector2Int(targetTile.x, targetTile.y);// spam lại list
            }

            for (int i = 0; i < currentTiles.Count; i++)
            {
                ItemTile sourceTile = currentTiles[i];// lưu vị trí các ô mới 
                sourceTile.x = shuffledLocations[i].x;
                sourceTile.y = shuffledLocations[i].y;
            }

            var match = CheckAnyMatch();
            if (match != null)
            {
                break;
            }
            else
            {
                if (shuffleCount == 1000)
                {
                    break;
                }

                shuffleCount++;
                Debug.LogWarning("Can not find match, shuffle again");
            }
            // check điều kiện đổi vị trí 
        }

        Vector3[] tileShuffleTargetPositions = new Vector3[currentTiles.Count];// sắp xếp xáo trộn vị trí 
        for (int i = 0; i < currentTiles.Count; i++)
        {
            ItemTile currentTile = currentTiles[i];
            tileShuffleTargetPositions[i] = GetPosition(currentTile.x, currentTile.y);// vị trí trộn đã lưu
        }

        return new ShuffleData()
        {
            tiles = currentTiles.ToArray(),
            positions = tileShuffleTargetPositions // lưu vị trí mới sau khi sắp xếp
        };
    }

    public Match CheckAnyMatch()// check trận
    {
        ItemTile tile1 = null;
        ItemTile tile2 = null;

        foreach (var list in tileGroups.Values)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                tile1 = list[i];

                for (int j = i + 1; j < list.Count; j++)
                {
                    tile2 = list[j];

                    var match = FindMatch(tile1.x, tile1.y, tile2.x, tile2.y);

                    if (match != null)
                    {
                        return match;
                    }
                }
            }
        }

        return null;
    }

    public Vector2 GetSize()//kích thước
    {
        return new Vector2(boardTileData.width * spacing, boardTileData.height * spacing);
    }

    public Vector3 GetPosition(int x, int y)// vị trí 
    {
        return new Vector3((x + 0.5f) * spacing + originPosition.x, (y + 0.5f) * spacing + originPosition.y, 0f);
    }

    public Vector3 GetPosition(Vector2Int location)// địa điểm 
    {
        return GetPosition(location.x, location.y);
    }

    public void Remove(int x, int y)// xóa
    {
        ItemTile tile = tiles[x][y];

        if (tile)
        {
            if (tileGroups.ContainsKey(tile.id))
            {
                var list = tileGroups[tile.id];
                list.Remove(tile);

                if (list.Count < 2)
                {
                    tileGroups.Remove(tile.id);
                }
            }

            tile.gameObject.SetActive(false);
            tile.OnRemoved();
        }

        tiles[x][y] = null;
        boardTileData.ClearTile(x, y);

        if (boardTileData.GetTileCount() == 0)
        {
            OnBoardClear?.Invoke();
        }
    }

    public Match FindMatch(int xa, int ya, int xb, int yb)// tải trận
    {
        return boardTileData.FindMatch(xa, ya, xb, yb);
    }
}
