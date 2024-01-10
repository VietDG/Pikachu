using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BoosterView : MonoBehaviour
{
    public Image icon;

    public Image ads;

    public Text amoutText;

    [NonSerialized] public DataBooster dataBooster;

    public Func<int> getCount;

    public Action<int> setCount;

    public abstract bool isUseBooster();

    public void UseBooster()
    {
        int count = getCount();

        if (count > 0)
        {
            if (isUseBooster())
                setCount(count - 1);

            StartAction();
        }
        else
        {
            //iconImage.gameObject.SetActive(false);
            //freeImage.gameObject.SetActive(true);
        }
    }

    public void StartAction()
    {
        int count = getCount();

        if (count != 0)
        {
            amoutText.gameObject.SetActive(true);
            amoutText.text = count.ToString();
            ads.gameObject.SetActive(false);
        }
        else
        {
            amoutText.gameObject.SetActive(false);
            amoutText.text = count.ToString();
            ads.gameObject.SetActive(true);
        }
    }
}
