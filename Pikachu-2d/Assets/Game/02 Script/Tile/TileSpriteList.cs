using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileSpriteList : ScriptableObject
{
    [SerializeField] Sprite[] spr;

    public Sprite GetSprite(int id)
    {
        return spr[id];
    }
}
