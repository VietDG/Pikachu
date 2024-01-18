using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public bool containTileIndex;
    public int row;
    public int col;
    public int width;
    public int height;
    public int condition;
    public int[] datas;
}
