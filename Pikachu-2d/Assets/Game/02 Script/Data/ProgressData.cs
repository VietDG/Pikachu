using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DataProgress", order = 1)]
public class ProgressData : MonoBehaviour
{
    public List<int> MaxValue = new List<int>();

    public int MaxProgress()
    {
        return MaxValue.Count;
    }
}
