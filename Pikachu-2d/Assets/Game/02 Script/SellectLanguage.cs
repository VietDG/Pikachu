using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellectLanguage : MonoBehaviour
{
    [SerializeField] GameObject _tick;
    [SerializeField] string _language;

    private void Start()
    {
        if (GameLanguage.Instance.crr_lang_code == _language)
        {
            _tick.SetActive(true);
        }
    }
}
