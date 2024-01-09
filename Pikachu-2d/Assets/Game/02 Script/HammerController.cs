using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    [NonSerialized]
    public ItemTile tile;

    public SpriteRenderer idleSpriteRenderer;

    public Transform hammerEffectTransform1;

    public Transform hammerEffectTransform2;

    public Animator hammerEffectAnimator1;

    public Animator hammerEffectAnimator2;

    //public ExplosionEffect explosionEffect1;

    //public ExplosionEffect explosionEffect2;

    private bool isTileActive = true;

    public static List<HammerController> activeHammers = new List<HammerController>();

    public static int animatingCount = 0;

    public void OnSpawn()
    {
        hammerEffectTransform1.localPosition = Vector3.zero;
        hammerEffectTransform2.localPosition = Vector3.zero;
        isTileActive = !tile.gameObject.activeSelf;

        idleSpriteRenderer.gameObject.SetActive(tile.gameObject.activeSelf);

        tile.RemovedEvent += OnTileRemoved;
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
        tile.RemovedEvent -= OnTileRemoved;
    }

    private void OnTileRemoved()
    {
        activeHammers.Add(this);
    }

    public void PlayEffect()
    {
        StartCoroutine(HammerEffectCoroutine());
    }

    private IEnumerator HammerEffectCoroutine()
    {
        var tileGroups = GameManager.Instance.GetTileGroups();
        var tileGroupList = new List<List<ItemTile>>(tileGroups.Values);

        if (tileGroupList.Count > 0)
        {
            var tilesWithId = tileGroupList[UnityEngine.Random.Range(0, tileGroupList.Count)];
            var tile1 = tilesWithId[0];
            var tile2 = tilesWithId[1];

            animatingCount++;

            idleSpriteRenderer.gameObject.SetActive(false);

            GamePlayLocker.Retain();

            hammerEffectAnimator1.gameObject.SetActive(true);
            hammerEffectAnimator2.gameObject.SetActive(true);

            bool isHammerAnimatorPlaying = false;

            hammerEffectTransform1.DOMove(tile1.transform.localPosition, 0.3f).SetEase(Ease.OutQuad).
                    OnComplete(() =>
                    {
                        isHammerAnimatorPlaying = true;
                        //  hammerEffectAnimator1.Play("HammerBeat");
                    });

            hammerEffectTransform2.DOMove(tile2.transform.localPosition, 0.3f).SetEase(Ease.OutQuad).
                    OnComplete(() =>
                    {
                        //  hammerEffectAnimator2.Play("HammerBeat");
                    });

            while (!isHammerAnimatorPlaying)
            {
                yield return null;
            }

            //while (hammerEffectAnimator1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            //{
            //    yield return null;
            //}

            //yield return null;

            hammerEffectAnimator1.gameObject.SetActive(false);
            hammerEffectAnimator2.gameObject.SetActive(false);


            //explosionEffect1.gameObject.SetActive(true);
            //explosionEffect2.gameObject.SetActive(true);

            GameManager.Instance.Remove(tile1.x, tile1.y);
            GameManager.Instance.Remove(tile2.x, tile2.y);

            GamePlayLocker.Release();

            animatingCount--;

            yield return new WaitForSeconds(2.5f);

            //explosionEffect1.gameObject.SetActive(false);
            //explosionEffect2.gameObject.SetActive(false);
        }

        OnDespawn();
    }

    public void Update()
    {
        transform.localPosition = tile.transform.localPosition;

        if (tile.gameObject.activeSelf != isTileActive)
        {
            isTileActive = tile.gameObject.activeSelf;
            idleSpriteRenderer.gameObject.SetActive(isTileActive);
        }
    }
}
