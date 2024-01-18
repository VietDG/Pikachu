using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public HamTile hamTilePrefab;

    public List<HamTile> hamPool = new List<HamTile>();

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
