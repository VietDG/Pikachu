using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public HamTile hamTilePrefab;
    public BoomTile boomTilePrefab;

    public List<HamTile> hamPool = new List<HamTile>();
    public List<BoomTile> boomPool = new List<BoomTile>();

    private int _boomCount;

    public void CreateHam(int value)
    {
        var tileList = new List<List<ItemTile>>(GameManager.Instance.GetTileDict().Values);
        var tilePos = tileList[UnityEngine.Random.Range(0, tileList.Count)];
        tileList.Shuffle();

        for (int i = 0; i < value; i++)
        {
            var ham = GetHamTile();
            ham.gameObject.SetActive(true);
            ham.itemTile = tileList[i][0];
            ham.InitHam();
        }
    }

    #region Boom
    public void InitBoom(int value)
    {
        _boomCount = value;
        CreateBoom();
    }

    public void CreateBoom()
    {
        var tileGroupList = new List<List<ItemTile>>((GameManager.Instance.GetTileDict().Values));
        if (tileGroupList.Count > 0)
        {
            var tiles = tileGroupList[UnityEngine.Random.Range(0, tileGroupList.Count)];
            if (tiles.Count > 0)
            {
                var tile = tiles[UnityEngine.Random.Range(0, tiles.Count)];
                if (_boomCount > 0)
                {
                    var bomb = GetBomb();
                    bomb.gameObject.SetActive(true);
                    bomb.itemTile = tileGroupList[0][0];
                    bomb.SpawnBoom();
                    _boomCount--;

                    if (_boomCount >= 1)
                    {
                        EventAction.OnRemoveBoom = CreateBoom;
                    }
                }
            }
        }
    }


    private BoomTile GetBomb()
    {
        for (int i = 0; i < boomPool.Count; i++)
        {
            if (boomPool[i].gameObject.activeSelf == false)
            {
                boomPool[i].gameObject.SetActive(true);
                return boomPool[i];
            }
        }
        return SpawnBomb();
    }

    private BoomTile SpawnBomb()
    {
        BoomTile bomb = Instantiate(boomTilePrefab);
        bomb.gameObject.SetActive(false);
        boomPool.Add(bomb);

        return bomb;
    }
    #endregion

    public IEnumerator StartMoveHam()
    {
        yield return null;

        while (HamTile.hamList.Count > 0)
        {
            for (int i = 0; i < HamTile.hamList.Count; i++)
            {
                HamTile.hamList[i].HamMovement();
            }
            HamTile.hamList.Clear();
            yield return new WaitUntil(() => HamTile.animCount <= 0);
        }
    }

    public void HamOff()
    {
        for (int i = 0; i < hamPool.Count; i++)
        {
            var ham = hamPool[i];
            if (ham.gameObject.activeSelf)
            {
                ham.HamActive();
                ham.gameObject.SetActive(false);
            }
        }
    }

    private HamTile GetHamTile()
    {
        for (int i = 0; i < hamPool.Count; i++)
        {
            if (hamPool[i].gameObject.activeSelf == false)
            {
                hamPool[i].gameObject.SetActive(true);
                return hamPool[i];
            }
        }
        return SpawnHamTile();
    }

    private HamTile SpawnHamTile()
    {
        HamTile ham = Instantiate(hamTilePrefab);
        ham.gameObject.SetActive(false);
        hamPool.Add(ham);
        return ham;
    }
}
