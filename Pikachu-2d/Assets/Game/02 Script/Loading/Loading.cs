using SS.View;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] TMP_Text _loadingtxt;
    [SerializeField] Slider _slider;
    private int percent;
    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        while (percent < 150)
        {
            percent++;
            _slider.value = (float)percent / 100;

            _loadingtxt.text = (percent >= 100) ? "100%" : $"{percent}%";
            yield return new WaitForSeconds(0.01f);
        }
        if (PlayerData.Instance.HighestLevel > 5)
            Manager.Load(DHome.SCENE_NAME);
        else
            Manager.Load(DGame.SCENE_NAME);

        SoundManager.Instance.PlayGameMusic();
    }
}
