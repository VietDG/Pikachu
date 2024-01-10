using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamTile : MonoBehaviour
{
    [Header("------------REFERENCE------------")]
    public SpriteRenderer ava;
    public Transform startTrans;
    public Transform endTrans;
    /// <summary>
    /// value
    /// </summary>
    public ItemTile itemTile { get; set; }
    private bool isActive = true;

    public static List<HamTile> hamList = new List<HamTile>();

    public static int animCount = 0;

    public void Update()
    {
        this.transform.localPosition = itemTile.transform.localPosition;

        if (itemTile.gameObject.activeSelf != isActive)
        {
            isActive = itemTile.gameObject.activeSelf;
            ava.gameObject.SetActive(isActive);
        }
    }

    public void InitHam()
    {
        startTrans.localPosition = Vector3.zero;
        endTrans.localPosition = Vector3.zero;
        isActive = !itemTile.gameObject.activeSelf;
        ava.gameObject.SetActive(itemTile.gameObject.activeSelf);
        EventAction.OnReMoveTile += RemoveTile;
    }

    public void HamMovement()
    {
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        var tileDict = GameManager.Instance.GetTileDict();
        var tileList = new List<List<ItemTile>>(tileDict.Values);

        if (tileList.Count > 0)
        {
            var getWidth = tileList[UnityEngine.Random.Range(0, tileList.Count)];
            var t1 = getWidth[0];
            var t2 = getWidth[1];

            animCount++;

            ava.gameObject.SetActive(false);

            MainController.Augment();

            startTrans.DOMove(t1.transform.localPosition, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        GameManager.Instance.RemoveTile(t1.index, t1.value);
                    });

            endTrans.DOMove(t2.transform.localPosition, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                GameManager.Instance.RemoveTile(t2.index, t2.value);
            });

            MainController.SetAllTileSize();

            animCount--;

            yield return new WaitForSeconds(2.5f);
        }

        HamActive();
    }


    public void HamActive()
    {
        gameObject.SetActive(false);
        EventAction.OnReMoveTile -= RemoveTile;
    }

    private void RemoveTile()
    {
        hamList.Add(this);
    }
}
