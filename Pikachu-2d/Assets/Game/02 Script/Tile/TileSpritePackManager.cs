using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpritePackManager : SingletonMonoBehaviour<TileSpritePackManager>
{
    //private int index = 0;

    [SerializeField] private TileSpritePack[] tileSpritePacks;

    public TileSpritePack GetTileSpritePack()
    {
        return tileSpritePacks[PlayerData.current.decorData.tileSpriteIndex % tileSpritePacks.Length];
        // thay ground
    }
}
