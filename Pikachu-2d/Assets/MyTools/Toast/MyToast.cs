using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyToast : MonoBehaviour
{
    #region Inspector Variables
    public SpriteRenderer sprite;
    public TMP_Text tvMess;
    #endregion

    #region Member Variables
    private Transform mTrans;
    private RectTransform _currentRect;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        _currentRect = this.GetComponent<RectTransform>();
    }
    #endregion

    #region Public Methods

    public void SetPosition()
    {
        _currentRect.anchoredPosition = Vector2.zero;
    }

    public void ShowMess(string value)
    {
        tvMess.text = value;
        gameObject.SetActive(true);
        StartCoroutine(Hide());
    }

    public IEnumerator Hide()
    {
        yield return new WaitForSeconds(2f);
        tvMess.text = "";
        //gameObject.SetActive(false);
        SimplePool.Despawn(this.gameObject);
    }
    #endregion
}
