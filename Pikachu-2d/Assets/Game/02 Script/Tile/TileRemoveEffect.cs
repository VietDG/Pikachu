using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRemoveEffect : SingletonMonoBehaviour<TileRemoveEffect>
{
    public GameObject prefab;

    private List<GameObject> pool = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();

        for (int i = 0; i < 5; i++)
        {
            var effect = SpawnParticleSystem();
            effect.SetActive(false);
        }
    }// tăt effect

    public GameObject GetParticleSystem()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeSelf == false)// kiểm tra xem pảticle có hoạt động hay k 
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        return SpawnParticleSystem();
    }

    public void ActivateEffectDelayDespawn(Vector3 position, float delayTime)
    {
        var effect = GetParticleSystem();
        effect.transform.localPosition = position;

        StartCoroutine(DespawnParticleSystem(effect, delayTime));//
    }

    private IEnumerator DespawnParticleSystem(GameObject gameObject, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.SetActive(false);// tắt effect
    }

    private GameObject SpawnParticleSystem()
    {
        GameObject effect = Instantiate(prefab, transform);
        effect.gameObject.SetActive(false);
        pool.Add(effect);

        return effect;// spam effect
    }
}