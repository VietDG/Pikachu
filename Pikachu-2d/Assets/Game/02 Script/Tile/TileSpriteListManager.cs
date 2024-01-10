using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpriteListManager : SingletonMonoBehaviour<TileSpriteListManager>
{
    [SerializeField] TileSpriteList[] _tileSpriteList;

    public TileSpriteList GetTileSpriteList()
    {
        return _tileSpriteList[PlayerData.playerData.spriteData.tileSpriteDataIndex % _tileSpriteList.Length];
    }
}
