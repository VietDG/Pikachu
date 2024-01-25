using DG.Tweening;
using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupTutHamer : SingletonPopup<PopupTutHamer>
{
    [SerializeField] Transform _trans1, _trans2;
    [SerializeField] Transform _hand;
    [SerializeField] TMP_Text _describeTxt;

    private Sequence sequence;

    public void Show()
    {
        StateGame.PauseGame();
        base.Show();
        SetHand();
    }

    private void Start()
    {
        _hand.position = new Vector3(_trans1.position.x, _trans1.position.y);
    }

    public void Close()
    {
        base.Hide();
        StateGame.Play();
    }

    public void SetHand()
    {
        float moveDuration = Mathf.Clamp((new Vector3(_trans2.position.x, _trans2.position.y, 0) - new Vector3(_trans1.position.x, _trans1.position.y)).magnitude / 10f, 0.5f, 1f);

        float moveDuration2 = Mathf.Clamp((new Vector3(_trans1.position.x, _trans1.position.y, 0) - new Vector3(_trans2.position.x, _trans2.position.y)).magnitude / 10f, 0.5f, 1f);

        sequence = DOTween.Sequence();

        // var tween1 = _hand.DOScale(1f, 0.35f);
        var move1 = _hand.DOMove(new Vector3(_trans2.position.x, _trans2.position.y), moveDuration).SetDelay(1f);

        // var tween2 = _hand.DOScale(1f, 0.35f);
        var move2 = _hand.DOMove(new Vector3(_trans1.position.x, _trans1.position.y), moveDuration2).SetDelay(1f);

        //    sequence.Append(tween1);
        //   sequence.Append(tween2);
        sequence.Append(move1);
        sequence.Append(move2);
        sequence.SetLoops(-1);
    }

}
