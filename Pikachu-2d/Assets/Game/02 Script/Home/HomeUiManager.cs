using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeUiManager : MonoBehaviour
{
    [SerializeField] TMP_Text _levelTxt, _levelOutlineTxt, _level, _levelOutline;

    private void Start()
    {
        SetLevel();
    }

    private void SetLevel()
    {
        _levelTxt.text = $"{PlayerData.Instance.HighestLevel}";
        _levelOutlineTxt.text = $"{PlayerData.Instance.HighestLevel}";
        _level.text = "level";
        _levelOutline.text = "level";
    }

}
