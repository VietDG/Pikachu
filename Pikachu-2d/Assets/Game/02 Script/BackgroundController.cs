using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController current;

    public ThemePool themePool;

    private void Start()
    {
        current = this;
        UpdateBackGroundSprite();
    }

    private void OnDestroy()
    {
        current = null;
    }

    public void UpdateBackGroundSprite()
    {
        GetComponent<SpriteRenderer>().sprite = themePool.GetThemeInfo(UserData.current.decorData.currentThemeID).sprite;
    }
}
