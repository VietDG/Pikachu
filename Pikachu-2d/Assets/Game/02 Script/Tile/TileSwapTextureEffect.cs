using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileSwapTextureEffect : MonoBehaviour
{
    public float duration = 0.28f;

    public float delayInterval = 0.05f;

    public void PlayEffect(ItemTile[][] tiles, int width, int height, Action CompleteAction = null)
    {
        StartCoroutine(PlayCoroutine(tiles, width, height, CompleteAction));
    }

    private IEnumerator PlayCoroutine(ItemTile[][] tiles, int width, int height, Action CompleteAction)
    {
        GamePlayLocker.Retain();
        // tăng lookcount
        UserData.current.decorData.tilePackIndex++;
        var spritePack = TileSpritePackManager.Instance.GetTileSpritePack();// thay ảnh 

        //for (int y = height - 1; y >= 0; y--)
        //{
        //    for (int x = 0; x < width; x++)
        //    {
        //        var tile = tiles[x][y];

        //        if (tile)
        //        {
        //            var sequence = DOTween.Sequence();

        //            var tween1 = tile.transform.DORotate(new Vector3(0f, 90f, 0), duration * 0.5f).OnComplete(() => tile.SetSprite(spritePack.Get(tile.id)));
        //            var tween2 = tile.transform.DORotate(new Vector3(0f, 0f, 0f), duration * 0.5f);

        //            sequence.Append(tween1);
        //            sequence.Append(tween2);
        //        }   
        //    }

        //    yield return new WaitForSeconds(delayInterval);
        //}

        for (int p = height - 1; p >= 0; p--)
        {
            int x = 0;
            int y = p;

            while (x >= 0 && y >= 0 && x < width && y < height)
            {
                var tile = tiles[x][y];// tạo ra bảng x,y 

                if (tile)
                {
                    var sequence = DOTween.Sequence();// tạo 1 chuỗi
                    

                    var tween1 = tile.transform.DORotate(new Vector3(0f, 90f, 0), duration * 0.5f).OnComplete(() => tile.SetSprite(spritePack.Get(tile.id)));
                    var tween2 = tile.transform.DORotate(new Vector3(0f, 180f, 0f), duration * 0.5f);

                    sequence.Append(tween1);
                    sequence.Append(tween2);// hiện nửa ảnh trên
                }
                //  quay ảnh khi đổi hình nửa dưới

                x++;
                y++;
            }

            yield return new WaitForSeconds(delayInterval);
        }

        for (int p = 1; p < width; p++)
        {
            int x = p;
            int y = 0;

            while (x >= 0 && y >= 0 && x < width && y < height)
            {
                var tile = tiles[x][y];

                if (tile)
                {
                    var sequence = DOTween.Sequence();

                    var tween1 = tile.transform.DORotate(new Vector3(0f, 90f, 0), duration * 0.5f).OnComplete(() => tile.SetSprite(spritePack.Get(tile.id)));
                    var tween2 = tile.transform.DORotate(new Vector3(0f, 0f, 0f), duration * 0.5f);

                    sequence.Append(tween1);
                    sequence.Append(tween2);
                }

                x++;
                y++;
            }
            // quay ảnh đôi hình nửa trên giữ hình nửa dưới
            yield return new WaitForSeconds(delayInterval);
        }

        //int cx = width / 2;
        //int cy = height / 2;
        //int radius = Mathf.Max(cx, cy) + 1;

        //for (int r = 0; r < radius; r++)
        //{
        //    int x = cx - r;
        //    int y = cy;

        //    while (x >= 0 && y >= 0 && x < width && y < height)
        //    {
        //        var tile = tiles[x][y];

        //        if (tile)
        //        {
        //            var sequence = DOTween.Sequence();

        //            var tween1 = tile.transform.DORotate(new Vector3(0f, 90f, 0), duration * 0.5f).OnComplete(() => tile.SetSprite(spritePack.Get(tile.id)));
        //            var tween2 = tile.transform.DORotate(new Vector3(0f, 0f, 0f), duration * 0.5f);

        //            sequence.Append(tween1);
        //            sequence.Append(tween2);
        //        }

        //        x++;
        //        y++;
        //    }

        //    yield return new WaitForSeconds(delayInterval);
        //}

        yield return null;

        CompleteAction?.Invoke();
        GamePlayLocker.Release();
    }
}
