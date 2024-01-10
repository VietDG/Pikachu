using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShuffleTile : MonoBehaviour
{
    public float moveDuration = 0.5f;

    public float delayTime = 0.3f;

    private float t;

    public IEnumerator PlayEffect(ItemTile[] tiles, Vector3[] targetPositions, Action CompleteAction = null)
    {
        MainController.Augment();

        Vector3[] tileStartPositions = new Vector3[tiles.Length];
        for (int i = 0; i < tiles.Length; i++)
        {
            tileStartPositions[i] = tiles[i].transform.localPosition;
        }

        t = 0f;
        DOTween.To(() => t, (value) => t = value, 1f, moveDuration).SetDelay(delayTime).
            OnUpdate(() =>
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].transform.localPosition = tileStartPositions[i] + (targetPositions[i] - tileStartPositions[i]) * t;
                }
            });

        yield return new WaitForSeconds(moveDuration + delayTime);

        MainController.SetAllTileSize();// độ dài đường thẳng từ điểm đầu tới điểm cuối
    }
}
