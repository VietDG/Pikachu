using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamTile : MonoBehaviour
{
    [Header("------------REFERENCE------------")]
    [NonSerialized]
    public ItemTile itemTile;
    public SpriteRenderer ava;
    public Transform startTrans;
    public Transform endTrans;

    public Animator animator1;

    public Animator animator2;

    private bool isActive = true;

    public static List<HamTile> hamList = new List<HamTile>();

    public static int animCount = 0;

    public void InitHam()
    {
        startTrans.localPosition = Vector3.zero;
        endTrans.localPosition = Vector3.zero;
        isActive = !itemTile.gameObject.activeSelf;
        ava.gameObject.SetActive(itemTile.gameObject.activeSelf);
        itemTile.OnRemoveTileEvent += RemoveTile;
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

            animator1.gameObject.SetActive(true);
            animator2.gameObject.SetActive(true);

            bool isHamerPlay = false;

            startTrans.DOMove(t1.transform.localPosition, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        isHamerPlay = true;
                        //   hammerEffectAnimator1.Play("Sprite");
                    });

            endTrans.DOMove(t2.transform.localPosition, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                // hammerEffectAnimator1.Play("Sprite");
            });

            while (!isHamerPlay)
            {
                yield return null;
            }
            GameManager.Instance.RemoveTile(t1.index, t1.value);
            GameManager.Instance.RemoveTile(t2.index, t2.value);
            while (animator1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
            yield return null;

            animator1.gameObject.SetActive(false);
            animator2.gameObject.SetActive(false);



            MainController.SetAllTileSize();
            animCount--;

            yield return new WaitForSeconds(2.5f);
        }

        HamActive();
    }

    public void HamActive()
    {
        gameObject.SetActive(false);
        itemTile.OnRemoveTileEvent -= RemoveTile;
    }

    private void RemoveTile()
    {
        hamList.Add(this);
    }

    public void Update()
    {
        this.transform.localPosition = itemTile.transform.localPosition;

        if (itemTile.gameObject.activeSelf != isActive)
        {
            isActive = itemTile.gameObject.activeSelf;
            ava.gameObject.SetActive(isActive);
        }
    }
}
