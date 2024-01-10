using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpriteListManager : SingletonMonoBehaviour<TileSpriteListManager>
{
    [SerializeField] TileSpriteList[] tileSpritePacks;

    public TileSpriteList GetTileSpritePack()
    {
        return tileSpritePacks[PlayerData.playerData.spriteData.tileSpriteDataIndex % tileSpritePacks.Length];
    }
}
