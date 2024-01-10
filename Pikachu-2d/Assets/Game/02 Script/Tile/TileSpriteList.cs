using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileSpriteList : ScriptableObject
{
    [SerializeField] Sprite[] sprites;

    public Sprite Get(int id)
    {
        return sprites[id];
    }
}
