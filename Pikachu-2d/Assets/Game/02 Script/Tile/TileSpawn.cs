using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    [SerializeField] float _duration = 0.5f;

    public void StartSpawn()
    {
        StartCoroutine(StartAction());
    }

    private IEnumerator StartAction()
    {
        MainController.Augment();

        ItemTile[][] itemTiles = GameManager.Instance.GetItemTile();
        int widths = GameManager.Instance.Width;
        int heights = GameManager.Instance.Height;

        for (int x = 0; x < widths; x++)
        {
            for (int y = 0; y < heights; y++)
            {
                ItemTile itemTile = itemTiles[x][y];
                if (itemTile != null)
                {
                    var trans = itemTile.transform;
                    trans.gameObject.SetActive(false);

                    Vector3 pos = trans.localPosition;
                    pos.z = -x * 0.01f - y * 0.02f;
                    trans.localPosition = pos * 0.5f;
                    trans.DOMove(pos, _duration).SetDelay(0.3f).OnStart(() => trans.gameObject.SetActive(true));
                }
            }
        }
        yield return new WaitForSeconds(_duration + 0.3f);
        MainController.SetAllTileSize();
    }
}
