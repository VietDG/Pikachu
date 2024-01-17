using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] float _duration = 0.3f;

    [SerializeField] float _delayTime = 0.05f;

    public void StartChangeSprite(ItemTile[][] itemTiles, int width, int height, Action callBack = null)
    {
        StartCoroutine(ActionChange(itemTiles, width, height, callBack));
    }

    private IEnumerator ActionChange(ItemTile[][] itemTiles, int width, int height, Action callBack)
    {
        MainController.Augment();
        PlayerData.Instance.TileSpriteIndex++;
        var pack = TileSpriteListManager.Instance.GetTileSpriteList();

        for (int h = height - 1; h >= 0; h--)
        {
            int x = 0;
            int y = h;

            while (x >= 0 && y >= 0 && x < width && y < height)
            {
                var itemTile = itemTiles[x][y];

                if (itemTile)
                {
                    var sequence = DOTween.Sequence();
                    var t1 = itemTile.transform.DORotate(new Vector3(0f, 90f, 0), _duration * 0.5f).OnComplete(() => itemTile.SetAva(pack.GetSprite(itemTile.idTile)));
                    var t2 = itemTile.transform.DORotate(new Vector3(0f, 0f, 0f), _duration * 0.5f);

                    sequence.Append(t1);
                    sequence.Append(t2);
                }
                x++;
                y++;
            }
            yield return new WaitForSeconds(_delayTime);
        }

        for (int w = 1; w < width; w++)
        {
            int x = w;
            int y = 0;

            while (x >= 0 && y >= 0 && x < width && y < height)
            {
                var itemTile = itemTiles[x][y];

                if (itemTile)
                {
                    var sequence = DOTween.Sequence();

                    var t1 = itemTile.transform.DORotate(new Vector3(0f, 90f, 0), _duration * 0.5f).OnComplete(() => itemTile.SetAva(pack.GetSprite(itemTile.idTile)));
                    var t2 = itemTile.transform.DORotate(new Vector3(0f, 0f, 0f), _duration * 0.5f);

                    sequence.Append(t1);
                    sequence.Append(t2);
                }
                x++;
                y++;
            }
            yield return new WaitForSeconds(_delayTime);
        }
        yield return null;
        callBack?.Invoke();
        MainController.SetAllTileSize();
    }
}
