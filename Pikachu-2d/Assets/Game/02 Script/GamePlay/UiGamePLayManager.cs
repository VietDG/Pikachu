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
    [SerializeField] TMP_Text _levelTxt, _levelOutLineTxt, _timeTxt;

    [SerializeField] Image _timeSliderImg;

    public GameObject _timeObj;

    [SerializeField] Image _iconTime;

    public GameObject _mask;
    public float _totalTime { get; set; }

    [Header("-----------------------VALUE--------------------------")]

    private int _runTime;

    private StringBuilder _stringBuilder = new StringBuilder();

    public void InitLevel()
    {
        _levelTxt.text = "Level " + PlayerData.Instance.HighestLevel.ToString();
        _levelOutLineTxt.text = _levelTxt.text;
    }

    public void SetMask(bool value)
    {
        _mask.SetActive(value);
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
        if (timer <= 60)
        {
            _timeTxt.color = Color.red;
            _iconTime.color = Color.red;
        }
    }

    public void SetModeTime(bool isCheck)
    {
        _timeObj.SetActive(isCheck);
    }

    public void OnClickSetting()
    {
        PopupSetting.Instance.Show();
        StateGame.PauseGame();
    }
}
