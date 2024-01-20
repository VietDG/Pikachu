using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiGamePLayManager : MonoBehaviour
{
    [Header("-----------------------REFERENCE----------------------")]
    [SerializeField] TMP_Text _levelTxt, _coinTxt, _timeTxt;

    [SerializeField] Image _timeSliderImg;

    [SerializeField] GameObject _timeObj;

    [SerializeField] Image _starSlider;

    public GameObject[] _starObj;

    [Header("-----------------------VALUE--------------------------")]

    private float _totalTime;

    private int _runTime;

    private StringBuilder _stringBuilder = new StringBuilder();

    private Tween _starTween;

    private Tween _endTween;

    private float _starCollectCount = 100;

    private float _starCollect;

    private float[] _starsProgress;

    private void Start()
    {
        UpdateCoin(PlayerData.Instance.TotalCoin);
    }

    public void InitLevel()
    {
        _levelTxt.text = "Level " + PlayerData.Instance.HighestLevel.ToString();
    }

    public void InitTimeToLevel(float time)
    {
        _totalTime = time;
    }

    public void SetTime(float timer)
    {
        _timeSliderImg.fillAmount = timer / _totalTime;// slider time 

        timer += 0f;

        if (_runTime != (int)timer)
        {
            _runTime = (int)timer;
            UitilyTime.SetMinuteAndSencond(_stringBuilder, _runTime);
            _timeTxt.text = _stringBuilder.ToString();
        }
    }


    public void SetProgressStarCollected(float[] progressValue)
    {
        for (int i = 0; i < _starObj.Length; i++)
        {
            _starObj[i].SetActive(false);
        }
        _starsProgress = progressValue;
    }

    public void SetStarCollect(float value, float maxValue)
    {
        _starCollect = value;
        _starCollectCount = maxValue;

        _starSlider.fillAmount = _starCollect / _starCollectCount;
    }

    public void StarCollected(float value, float time)
    {
        _starCollect = Mathf.Min(_starCollectCount, _starCollect + value);

        _starTween = _endTween = _starSlider.DOFillAmount(_starCollect / _starCollectCount, 0.5f)/*.SetDelay(time)*/.
        SetEase(Ease.Linear).OnUpdate(() =>
            {
                for (int i = 0; i < _starObj.Length; i++)
                {
                    _starObj[i].SetActive(_starSlider.fillAmount >= _starsProgress[i]);
                }
            }).
            OnStart(() =>
            {
                if (_endTween != null && !_endTween.IsActive())
                {
                    _endTween.Kill();
                }
                _endTween = _starTween;
            });
    }

    public void SetModeTime(bool isCheck)
    {
        _timeObj.SetActive(isCheck);
    }

    private void UpdateCoin(object value)
    {
        _coinTxt.text = value.ToString();
    }

    public void OnClickSetting()
    {
        PopupSetting.Instance.Show();
        StateGame.PauseGame();
    }
}
