using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public HamTile hamTilePrefab;

    private List<HamTile> hamList = new List<HamTile>();

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

        while (hamList.Count > 0)
        {
            for (int i = 0; i < hamList.Count; i++)
            {
                hamList[i].HamMovement();
            }
            hamList.Clear();
            yield return new WaitUntil(() => HamTile.animCount <= 0);
        }
    }

    public void HamOff()
    {
        for (int i = 0; i < hamList.Count; i++)
        {
            var ham = hamList[i];
            if (ham.gameObject.activeSelf)
            {
                ham.HamActive();
                ham.gameObject.SetActive(false);
            }
        }
    }

    private HamTile GetHamTile()
    {
        for (int i = 0; i < hamList.Count; i++)
        {
            if (hamList[i].gameObject.activeSelf == false)
            {
                hamList[i].gameObject.SetActive(true);
                return hamList[i];
            }
        }
        return SpawnHamTile();
    }

    private HamTile SpawnHamTile()
    {
        HamTile ham = Instantiate(hamTilePrefab);
        ham.gameObject.SetActive(false);
        hamList.Add(ham);
        return ham;
    }
}
