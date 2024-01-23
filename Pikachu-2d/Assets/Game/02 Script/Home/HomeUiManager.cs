using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUiManager : MonoBehaviour
{
    [SerializeField] TMP_Text _levelTxt, _levelOutlineTxt, _level, _levelOutline;
    [SerializeField] Image _bg;

    private void Start()
    {
        SetLevel();
        SetBGHome();
    }

    private void SetLevel()
    {
        _levelTxt.text = $"{PlayerData.Instance.HighestLevel}";
        _levelOutlineTxt.text = $"{PlayerData.Instance.HighestLevel}";
        _level.text = "level";
        _levelOutline.text = "level";
    }

    public void SetBGHome()
    {
        _bg.sprite = BackGroundManager.Instance._themeList.GetBg();
    }

}
