using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlot : MonoBehaviour
{
    [Range(0f, 1f)]
    public float ratio = 1f;

    public Image image;

    public virtual void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
        if (sprite)
        {
            image.sprite = sprite;
            image.gameObject.SetActive(true);
        }
        else
        {
            image.sprite = null;
            image.gameObject.SetActive(false);
        }
    }

    public virtual void FitSize()
    {
        FitSize(GetComponent<RectTransform>().sizeDelta);
    }

    public virtual void FitSize(Vector2 sizeDelta)
    {
        image.SetNativeSize();
        Vector2 size = image.rectTransform.sizeDelta;

        //if (image.sprite)
        //{
        //    Sprite sprite = image.sprite;
        //    Vector2 pivotRatio = sprite.pivot / sprite.rect.size;

        //    if (pivotRatio.x > 0.5f) 
        //        size.x = size.x * pivotRatio.x * 2f;
        //    else 
        //        size.x = size.x * (1f - pivotRatio.x) * 2f;

        //    if (pivotRatio.y > 0.5f)
        //        size.y = size.y * pivotRatio.y * 2f;
        //    else
        //        size.y = size.y * (1f - pivotRatio.y) * 2f;

        //    //image.rectTransform.pivot = pivotRatio;
        //}

        float spriteSizeRatio = size.x / size.y;
        Vector2 parentSize = sizeDelta;
        Vector2 predictSize = new Vector2(parentSize.x, parentSize.x / spriteSizeRatio);

        if (predictSize.y > parentSize.y)
        {
            predictSize *= parentSize.y / predictSize.y;
        }

        image.rectTransform.sizeDelta = predictSize * ratio;
    }

#if UNITY_EDITOR
    [ContextMenu("Execute")]
    private void Fit_Internal()
    {
        FitSize();
    }
#endif
}
