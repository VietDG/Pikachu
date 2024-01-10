using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ThemePool : ScriptableObject
{
    [Serializable]
    public class ThemeInfo
    {
        public UnityEngine.Sprite sprite;
        public UnityEngine.Sprite icon;
        public string id;

        [NonSerialized] public int index;
    }

    public List<ThemeInfo> data = new List<ThemeInfo>();

    private Dictionary<string, ThemeInfo> lookupTable = new Dictionary<string, ThemeInfo>();

    private void OnEnable()
    {
        lookupTable.Clear();

        for (int i = 0; i < data.Count; i++)
        {
            ThemeInfo themeInfo = data[i];

            if (!lookupTable.ContainsKey(themeInfo.id))
            {
                lookupTable.Add(themeInfo.id, themeInfo);
            }
            else
            {
                Debug.Log("Theme pool duplicate ID: " + themeInfo.id);
            }
        }
    }

    public ThemeInfo GetThemeInfo(string id)
    {
        lookupTable.TryGetValue(id, out ThemeInfo ret);
        return ret;
    }
}
