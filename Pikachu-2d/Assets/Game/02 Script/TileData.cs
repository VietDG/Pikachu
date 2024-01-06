using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : Singleton<TileData>
{
    public int[][] data;

    public const int EmptyTileCode = -1;

    public int width = 6;

    public int height = 19;

    public bool canShuffleOnInit = true;

    private int kind = 10;

    private int tileCount;

    private List<Vector2Int> shuffleLocations = new List<Vector2Int>();

    public void Initialize(LevelConfig levelConfig, BoardConfig boardConfig, int maxKind)
    {
        // tạo bảng levelconfig:hình dạnng thời gian,... ,bảng cấu hình boardconfig
        shuffleLocations.Clear();//xóa list
        tileCount = 0;

        width = Mathf.Max(2, boardConfig.row);// trả về giá trị lớn nhất 
        height = Mathf.Max(2, boardConfig.col);// trả về giá trị lớn nhất 
        kind = levelConfig.kind;//trả về kiểu int
        canShuffleOnInit = !boardConfig.containTileIndex;// có thể  xáo trộn bảng 

        data = new int[width][]; // tạo chiều rộng của bảng
        for (int x = 0; x < width; x++)
        {
            data[x] = new int[height];       // tạo ra chiều cao 
        }

        if (canShuffleOnInit)// có thể xáo trộn 
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = (height - y - 1) * width + x;// thay đổi vị trí chiều cao = chiều cao-y tăng dần -1 nhân chiều rộng + x
                    if (boardConfig.datas[index] == 0)// khi trừ =0 thì
                    {
                        shuffleLocations.Add(new Vector2Int(x, y));// add 1 vi trí mới 

                        tileCount++;
                    }
                    else
                    {
                        data[x][y] = EmptyTileCode;
                    }
                }
            }
        }
        else// k thể xáo trộn 
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = (height - y - 1) * width + x;
                    if (boardConfig.datas[index] >= 0)
                    {
                        data[x][y] = boardConfig.datas[index];// giữ nguyên k thay đổi 

                        tileCount++;// tăng tilecount
                    }
                    else
                    {
                        data[x][y] = EmptyTileCode;
                    }
                }
            }
        }
    }

    public void ShuffleLocation()
    {
        shuffleLocations.Shuffle();// tạo list ngẫu nhiên vị trí

        List<int> kinds = new List<int>();
        for (int i = 0; i < kind; i++)
        {
            kinds.Add(i);
        }
        kinds.Shuffle();// add thành phần mới vào list

        for (int i = 0; i < shuffleLocations.Count; i += 2)
        {
            int kindIndex = (i / 2) % kind;

            Vector2Int loc1 = shuffleLocations[i];
            Vector2Int loc2 = shuffleLocations[i + 1];

            data[loc1.x][loc1.y] = kinds[kindIndex];
            data[loc2.x][loc2.y] = kinds[kindIndex];
        }
    }

    public int GetTileCount()
    {
        return tileCount;// nhập số lượng ô 
    }

    public void ClearTile(int x, int y)
    {
        data[x][y] = EmptyTileCode;
        tileCount--;//xóa ô
    }

    public void SetTileData(int x, int y, int id)
    {
        data[x][y] = id;// lấy ID cho mỗi ô gạch
    }

    public Match FindMatch(int xa, int ya, int xb, int yb)//tìm trận 
    {
        Match match = null;
        // 1 hang
        if (xa == xb || ya == yb)
        {
            if (xa == xb && IsVerticalLineClear(xa, ya, yb))
            {

                match = new Match();
                match.AddLocation(xa, ya);
                match.AddLocation(xb, yb);

                return match;
            }
            else if (ya == yb && IsHorizontalLineClear(xa, xb, ya))
            {
                match = new Match();
                match.AddLocation(xa, ya);
                match.AddLocation(xb, yb);
                return match;
            }
        }

        // 2 hafng
        if (IsPerpendicularClear_VerticalSearch(xa, ya, xb, yb))
        {
            match = new Match();
            match.AddLocation(xa, ya);
            match.AddLocation(xa, yb);
            match.AddLocation(xb, yb);

            return match;
        }
        else if (IsPerpendicularClear_HorizontalSearch(xa, ya, xb, yb))
        {
            match = new Match();
            match.AddLocation(xa, ya);
            match.AddLocation(xb, ya);
            match.AddLocation(xb, yb);

            return match;
        }

        //3 hàng
        for (int x = xa - 1; x >= -1; x--)
        {
            if (IsTileEmpty(x, ya))
            {
                if (IsPerpendicularClear_VerticalSearch(x, ya, xb, yb))
                {
                    match = new Match();
                    match.AddLocation(xa, ya);
                    match.AddLocation(x, ya);
                    match.AddLocation(x, yb);
                    match.AddLocation(xb, yb);

                    return match;
                }
            }
            else
            {
                break;
            }
        }
        //4 hàng
        for (int x = xa + 1; x <= width; x++)
        {
            if (IsTileEmpty(x, ya))
            {
                if (IsPerpendicularClear_VerticalSearch(x, ya, xb, yb))
                {
                    match = new Match();
                    match.AddLocation(xa, ya);
                    match.AddLocation(x, ya);
                    match.AddLocation(x, yb);
                    match.AddLocation(xb, yb);

                    return match;
                }
            }
            else
            {
                break;
            }
        }
        //5 hàng
        for (int y = ya - 1; y >= -1; y--)
        {
            if (IsTileEmpty(xa, y))
            {
                if (IsPerpendicularClear_HorizontalSearch(xa, y, xb, yb))
                {
                    match = new Match();
                    match.AddLocation(xa, ya);
                    match.AddLocation(xa, y);
                    match.AddLocation(xb, y);
                    match.AddLocation(xb, yb);

                    return match;
                }
            }
            else
            {
                break;
            }
        }
        // 6 hàng
        for (int y = ya + 1; y <= height; y++)
        {
            if (IsTileEmpty(xa, y))
            {
                if (IsPerpendicularClear_HorizontalSearch(xa, y, xb, yb))
                {
                    match = new Match();
                    match.AddLocation(xa, ya);
                    match.AddLocation(xa, y);
                    match.AddLocation(xb, y);
                    match.AddLocation(xb, yb);

                    return match;
                }
            }
            else
            {
                break;
            }
        }

        return null;
    }

    private bool IsPerpendicularClear_VerticalSearch(int xa, int ya, int xb, int yb)
    {
        // tìm kiếm hàng dọc vuông góc
        if (IsTileEmpty(xa, yb) && IsVerticalLineClear(xa, ya, yb) && IsHorizontalLineClear(xa, xb, yb))
        {
            return true;
        }

        return false;
    }

    private bool IsPerpendicularClear_HorizontalSearch(int xa, int ya, int xb, int yb)
    {
        if (IsTileEmpty(xb, ya) && IsVerticalLineClear(xb, ya, yb) && IsHorizontalLineClear(xa, xb, ya))
        {
            return true;
        }

        return false;
    }

    private bool IsHorizontalLineClear(int xa, int xb, int y)
    {
        int x1 = xa;
        int x2 = xb;

        if (xb < xa)
        {
            x1 = xb;
            x2 = xa;
        }

        for (int x = x1 + 1; x < x2; x++)
        {
            if (!IsTileEmpty(x, y))
                return false;
        }

        return true;
    }

    private bool IsVerticalLineClear(int x, int ya, int yb)
    {
        int y1 = ya;
        int y2 = yb;

        if (yb < ya)
        {
            y1 = yb;
            y2 = ya;
        }

        for (int y = y1 + 1; y < y2; y++)
        {
            if (!IsTileEmpty(x, y))
                return false;
        }

        return true;
    }

    private bool IsTileEmpty(int x, int y)
    {
        return x < 0 || x >= width || y < 0 || y >= height || data[x][y] == EmptyTileCode;
    }
}

public class Match
{
    public void AddLocation(int x, int y)
    {
        locations.Add(new Vector2Int(x, y));
    }

    public List<Vector2Int> locations = new List<Vector2Int>();
}

public static class IListExtensions
{
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
