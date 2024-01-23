using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ThemePack : ScriptableObject
{
    public Sprite[] _bg;

    public Sprite GetBg()
    {
        return _bg[PlayerData.Instance.ThemeIndex % _bg.Length];
    }
}
