using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    [SerializeField] private ProgressData _progressData;

    public ProgressData GetProgressGift()
    {
        return _progressData;
    }
}
