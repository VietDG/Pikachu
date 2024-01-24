using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    public HandTutController handTut;

    public GameObject failGuilde1, failGuilde2;

    private ItemTile[][] tileTut;

    private int _width, _height, _matchtileCount, _failtileCount;

    private float _delayTime = 0.3f;

    private void Matchtile(MatchT matcht)
    {
        _matchtileCount++;
    }

    private void FailedTile(ItemTile item1, ItemTile item2)
    {
        _failtileCount++;
    }

    public void Skip()
    {
        handTut.Skip();
    }

    public void OnclickSkip()
    {
        Skip();
        NextTile();
        this.gameObject.SetActive(false);

        EventAction.OnMatchTile -= Matchtile;
        EventAction.OnMatchTileFail -= FailedTile;
    }

    public void StartTut()
    {
        gameObject.SetActive(true);

        _width = GameManager.Instance.Width;
        _height = GameManager.Instance.Height;
        tileTut = GameManager.Instance.itemTiles;

        EventAction.OnMatchTile += Matchtile;
        EventAction.OnMatchTileFail += FailedTile;

        StartCoroutine(PlayeTutLevel1());
        GameController.Instance.uiGamePlayManager._mask.SetActive(true);

    }

    private IEnumerator PlayeTutLevel1()
    {
        MainController.Augment();

        yield return new WaitForSeconds(1f);

        MainController.SetAllTileSize();

        Vector2Int posA = new Vector2Int(1, 2);
        Vector2Int posB = new Vector2Int(3, 2);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_matchtileCount == 0)
        {
            yield return null;
        }

        Skip();
        yield return new WaitForSeconds(_delayTime);

        posA = new Vector2Int(3, 3);
        posB = new Vector2Int(4, 2);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_matchtileCount == 1)
        {
            yield return null;
        }

        Skip();
        yield return new WaitForSeconds(_delayTime);

        posA = new Vector2Int(0, 2);
        posB = new Vector2Int(4, 4);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_matchtileCount == 2)
        {
            yield return null;
        }

        Skip();

        yield return new WaitForSeconds(_delayTime);

        posA = new Vector2Int(0, 1);
        posB = new Vector2Int(1, 0);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_failtileCount == 0)
        {
            yield return null;
        }

        Skip();

        failGuilde1.SetActive(true);

        yield return new WaitForSeconds(0.8f);
        failGuilde1.SetActive(failGuilde2);

        yield return new WaitForSeconds(_delayTime);

        posA = new Vector2Int(1, 1);
        posB = new Vector2Int(1, 4);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_failtileCount == 1)
        {
            yield return null;
        }

        Skip();

        failGuilde2.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        failGuilde2.SetActive(failGuilde2);

        yield return new WaitForSeconds(_delayTime);

        posA = new Vector2Int(2, 3);
        posB = new Vector2Int(3, 4);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_matchtileCount == 3)
        {
            yield return null;
        }

        Skip();

        yield return new WaitForSeconds(_delayTime);

        posA = new Vector2Int(1, 1);
        posB = new Vector2Int(1, 4);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_matchtileCount == 4)
        {
            yield return null;
        }

        Skip();

        yield return new WaitForSeconds(_delayTime);

        posA = new Vector2Int(1, 3);
        posB = new Vector2Int(4, 1);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_matchtileCount == 5)
        {
            yield return null;
        }

        Skip();

        posA = new Vector2Int(0, 1);
        posB = new Vector2Int(1, 0);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_matchtileCount == 6)
        {
            yield return null;
        }

        Skip();

        yield return new WaitForSeconds(_delayTime);

        posA = new Vector2Int(0, 0);
        posB = new Vector2Int(4, 3);

        SetHighlightTile(posA, posB);
        SetPosTile(posA, posB);

        while (_matchtileCount == 7)
        {
            yield return null;
        }

        OnclickSkip();
    }

    private void SetHighlightTile(Vector2Int posA, Vector2Int posB)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                ItemTile itemTile = tileTut[i][j];
                if (itemTile != null)
                {
                    if ((i == posA.x && j == posA.y) || (i == posB.x && j == posB.y))
                    {
                        SetLayer(itemTile, true);
                    }
                    else
                    {
                        SetLayer(itemTile, false);
                    }
                }
            }
        }
    }

    private void SetPosTile(Vector2Int posA, Vector2Int posB)
    {
        handTut.SetAnim(tileTut[posA.x][posA.y].transform.localPosition, tileTut[posB.x][posB.y].transform.localPosition);
    }

    private void NextTile()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                ItemTile itemTile = tileTut[i][j];
                if (itemTile != null)
                {
                    SetLayer(itemTile, true);
                }
            }
        }
    }

    private void SetLayer(ItemTile itemTile, bool isLayer)
    {
        itemTile.enabled = isLayer;
        if (isLayer)
        {
            itemTile._animator.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            itemTile.ava.color = Color.white;
        }
        else
        {
            itemTile._animator.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
            itemTile.ava.color = new Color(0.4f, 0.4f, 0.4f, 1f);
        }
    }
}
