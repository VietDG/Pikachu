using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutController : MonoBehaviour
{
    public Transform trans;

    private Sequence sequence;

    public void Skip()
    {
        gameObject.SetActive(false);
        if (sequence != null && sequence.IsActive())
            sequence.Kill();
        sequence = null;
    }

    public void SetAnim(Vector3 posA, Vector3 posB)
    {
        float moveDuration = Mathf.Clamp((posB - posA).magnitude / 10f, 0.5f, 1f);

        gameObject.SetActive(true);
        trans.position = posA;
        sequence = DOTween.Sequence();

        var tween1 = trans.DOScale(1f, 0.35f);
        var move1 = trans.DOMove(posB, moveDuration);

        var tween2 = trans.DOScale(1f, 0.35f);
        var move2 = trans.DOMove(posA, moveDuration).SetDelay(0.25f);

        sequence.Append(tween1);
        sequence.Append(move1);
        sequence.Append(tween2);
        sequence.Append(move2);
        sequence.SetLoops(-1);
    }
}
