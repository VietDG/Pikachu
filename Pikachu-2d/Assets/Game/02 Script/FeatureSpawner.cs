using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureSpawner : MonoBehaviour
{
    public BombController bombPrefab;

    public HammerController hammerPrefab;

    private List<BombController> bombPool = new List<BombController>();

    private List<HammerController> hammerPool = new List<HammerController>();

    private int remainingBomb;

    public void CreateBomb(int amount)
    {
        remainingBomb = amount;

        CreateBombSequence();
    }

    private void CreateBombSequence()
    {
        var tileGroupList = new List<List<ItemTile>>(GameManager.Instance.GetTileGroups().Values);
        if (tileGroupList.Count > 0)
        {
            var tiles = tileGroupList[UnityEngine.Random.Range(0, tileGroupList.Count)];
            if (tiles.Count > 0)
            {
                var tile = tiles[UnityEngine.Random.Range(0, tiles.Count)];
                if (remainingBomb > 0)
                {
                    var bomb = GetBomb();
                    bomb.gameObject.SetActive(true);
                    bomb.tile = tileGroupList[0][0];
                    bomb.OnSpawn();
                    remainingBomb--;

                    if (remainingBomb >= 1)
                    {
                        bomb.RemovedEvent = CreateBombSequence;
                    }
                }
            }
        }
    }

    public void CreateHammer(int amount)
    {
        var tileGroupList = new List<List<ItemTile>>(GameManager.Instance.GetTileGroups().Values);
        var tilesWithId = tileGroupList[UnityEngine.Random.Range(0, tileGroupList.Count)];

        tileGroupList.Shuffle();

        for (int i = 0; i < amount; i++)
        {
            var hammer = GetHammer();
            hammer.gameObject.SetActive(true);

            hammer.tile = tileGroupList[i][0];
            hammer.OnSpawn();
        }
    }

    public IEnumerator PostTileMatchSucceeded()
    {
        yield return null;

        while (HammerController.activeHammers.Count > 0)
        {
            for (int i = 0; i < HammerController.activeHammers.Count; i++)
            {
                HammerController.activeHammers[i].PlayEffect();
            }

            Debug.Log("Hammer size: " + HammerController.activeHammers.Count);

            HammerController.activeHammers.Clear();

            yield return new WaitUntil(() => HammerController.animatingCount <= 0);
        }
    }

    public void ResetAllPools()
    {
        for (int i = 0; i < bombPool.Count; i++)
        {
            var bomb = bombPool[i];
            if (bomb.gameObject.activeSelf)
            {
                bomb.OnDespawn();
                bomb.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < hammerPool.Count; i++)
        {
            var hammer = hammerPool[i];
            if (hammer.gameObject.activeSelf)
            {
                hammer.OnDespawn();
                hammer.gameObject.SetActive(false);
            }
        }
    }

    private BombController GetBomb()
    {
        for (int i = 0; i < bombPool.Count; i++)
        {
            if (bombPool[i].gameObject.activeSelf == false)
            {
                bombPool[i].gameObject.SetActive(true);
                return bombPool[i];
            }
        }

        return SpawnBomb();
    }

    private BombController SpawnBomb()
    {
        BombController bomb = Instantiate(bombPrefab);
        bomb.gameObject.SetActive(false);
        bombPool.Add(bomb);

        return bomb;
    }

    private HammerController GetHammer()
    {
        for (int i = 0; i < hammerPool.Count; i++)
        {
            if (hammerPool[i].gameObject.activeSelf == false)
            {
                hammerPool[i].gameObject.SetActive(true);
                return hammerPool[i];
            }
        }

        return SpawnHammer();
    }

    private HammerController SpawnHammer()
    {
        HammerController hammer = Instantiate(hammerPrefab);
        hammer.gameObject.SetActive(false);
        hammerPool.Add(hammer);

        return hammer;
    }
}
