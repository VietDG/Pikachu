using SS.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] TMP_Text _loadingtxt;
    [SerializeField] Slider _slider;
    private int percent;
    [SerializeField] Image _bgLoading;

    private void Awake()
    {
        _bgLoading.sprite = BackGroundManager.Instance._themeList.GetBg();
    }

    void Start()
    {
        StartCoroutine(Load());
        StartCoroutine(LoadingText());
    }

    IEnumerator Load()
    {
        while (percent < 150)
        {
            percent++;
            _slider.value = (float)percent / 100;

            // _loadingtxt.text = (percent >= 100) ? "100%" : $"{percent}%";
            yield return new WaitForSeconds(0.01f);
        }
        if (PlayerData.Instance.HighestLevel > 5)
            Manager.Load(DHome.SCENE_NAME);
        else
            Manager.Load(DGame.SCENE_NAME);

        SoundManager.Instance.PlayGameMusic();
    }

    private IEnumerator LoadingText()
    {
        while (true)
        {
            _loadingtxt.text = "Loading.";
            yield return new WaitForSeconds(0.3f);
            _loadingtxt.text = "Loading..";
            yield return new WaitForSeconds(0.3f);
            _loadingtxt.text = "Loading...";
            yield return new WaitForSeconds(0.3f);
        }
    }
}

