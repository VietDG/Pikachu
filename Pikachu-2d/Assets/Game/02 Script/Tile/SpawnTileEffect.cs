using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTileEffect : MonoBehaviour
{
    public float duration = 0.5f;

    public void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        GamePlayLocker.Retain();// unlock màn chơi 

        ItemTile[][] tiles = GameManager.Instance.GetTiles();// tạo bảng  
        int width = GameManager.Instance.width;
        int height = GameManager.Instance.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                ItemTile tile = tiles[x][y];
                if (tile != null)
                {
                    var transform = tile.transform;
                    transform.gameObject.SetActive(false);

                    Vector3 position = transform.localPosition;
                    position.z = -x * 0.01f - y * 0.02f;
                    transform.localPosition = position * 0.5f;
                    transform.DOMove(position, duration).SetDelay(0.3f).OnStart(() => transform.gameObject.SetActive(true));
                }
            }
        }

        yield return new WaitForSeconds(duration + 0.3f);

        GamePlayLocker.Release();
    }
}
