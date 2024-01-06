using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBottomUIView : MonoBehaviour
{
    public Image[] images;

    [SerializeField] private Image[] selectedImages;

    private Image[] normalImages;

    private Canvas canvas;

    private int selectedIndex;

    private bool locked;

    private void Start()
    {
        canvas = GetComponent<Canvas>();

        selectedImages = new Image[3];
        normalImages = new Image[3];

        for (int i = 0; i < images.Length; i++)
        {
            normalImages[i] = images[i].transform.GetChild(0).GetComponent<Image>();
            selectedImages[i] = images[i].transform.GetChild(1).GetComponent<Image>();
        }

        SetSelect(1);

    }

    private void OnDestroy()
    {
    }



    public void ButtonDailyBonusPress()
    {
        if (SetSelect(0))
        {
            Debug.Log("1111");
        }
    }

    public void ButtonHomePress()
    {
        if (SetSelect(1))
        {
        }
    }

    public void ButtonThemePress()
    {
        if (SetSelect(2))
        {
        }
    }

    private bool SetSelect(int index)
    {
        if (selectedIndex != index && locked == false)
        {
            StopAllCoroutines();
            StartCoroutine(LockCoroutine());

            selectedIndex = index;

            for (int i = 0; i < images.Length; i++)
            {
                bool active = i == selectedIndex;

                images[i].color = active ? Color.white : Color.clear;
                selectedImages[i].gameObject.SetActive(active);
                normalImages[i].gameObject.SetActive(!active);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator LockCoroutine()
    {
        locked = true;

        yield return new WaitForSeconds(0.35f);

        locked = false;
    }
}
