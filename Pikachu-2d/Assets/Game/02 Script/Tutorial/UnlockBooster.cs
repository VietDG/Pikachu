using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockBooster : MonoBehaviour
{
    public int _levelUnlock;
    [SerializeField] private GameObject _mask, _amoutBooster;
    [SerializeField] TMP_Text _levelTxt, _levelOutlineTxt;

    private void Start()
    {
        if (PlayerData.Instance.HighestLevel < _levelUnlock)
        {
            _mask.gameObject.SetActive(true);
            _levelTxt.text = $"level {_levelUnlock}";
            _levelOutlineTxt.text = _levelTxt.text;
            _amoutBooster.SetActive(false);
        }
    }
}
