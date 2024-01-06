using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileSpritePack : ScriptableObject
{
    [SerializeField] private Sprite[] sprites;

    public Sprite Get(int id)
    {
        return sprites[id];
    }
}
