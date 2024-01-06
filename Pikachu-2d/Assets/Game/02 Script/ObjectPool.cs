using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool>
{
    Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>> ();

    public static GameObject Spawn (GameObject prefab)
    {
        GameObject result = null;
        if (!Instance.pools.ContainsKey (prefab.name)) Instance.pools.Add (prefab.name, new Queue<GameObject> ());
        if (Instance.pools [prefab.name].Count != 0)
        {
            result = Instance.pools [prefab.name].Dequeue ();
            result.SetActive (true);
        }
        else
        {
            result = Object.Instantiate(prefab);
            result.name = prefab.name;
        }
        return result;
    }

    public static GameObject Spawn (GameObject prefab, Transform parent)
    {
        GameObject result = Spawn (prefab);
        result.transform.SetParent (parent);
        return result;
    }

    public static GameObject Spawn (GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject result = Spawn (prefab);
        result.transform.position = position;
        result.transform.rotation = rotation;
        return result;
    }

    public static void Despawn (GameObject prefab)
    {
        if (!Instance.pools.ContainsKey (prefab.name)) Instance.pools.Add (prefab.name, new Queue<GameObject> ());
        Instance.pools [prefab.name].Enqueue (prefab);
        prefab.transform.SetParent(null);
        prefab.SetActive (false);
    }

    public static void Prespawn (GameObject prefab, int count)
    {
        if (Instance.pools.ContainsKey (prefab.name)) return;
        Instance.pools.Add (prefab.name, new Queue<GameObject> ());
        for (int i = 0; i < count; i++)
        {
            GameObject target = Object.Instantiate(prefab);
            Instance.pools [prefab.name].Enqueue (target);
            target.name = prefab.name;
            target.SetActive (false);
        }
    }
}