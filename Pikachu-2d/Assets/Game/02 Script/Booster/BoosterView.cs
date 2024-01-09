using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BoosterView : MonoBehaviour
{
    public BoosterType boosterType;

    public Image iconImage;

    public Image freeImage;

    public Text countText;

    [NonSerialized] public BoosterData boosterData;

    public Func<int> GetCount;

    public Action<int> SetCount;

    public abstract bool Use();

    public void OnClick()
    {
        int count = GetCount();

        if (count > 0)
        {
            if (Use())
                SetCount(count - 1);

            UpdateState();
        }
        else
        {
            //iconImage.gameObject.SetActive(false);
            //freeImage.gameObject.SetActive(true);
        }
    }

    public void UpdateState()
    {
        int count = GetCount();

        if (count != 0)
        {
            countText.gameObject.SetActive(true);
            countText.text = count.ToString();
            freeImage.gameObject.SetActive(false);
        }
        else
        {
            countText.gameObject.SetActive(false);
            countText.text = count.ToString();
            freeImage.gameObject.SetActive(true);
        }
    }
}
