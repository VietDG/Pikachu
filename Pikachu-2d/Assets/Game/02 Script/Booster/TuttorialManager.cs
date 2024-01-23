using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuttorialManager : MonoBehaviour
{

    [SerializeField] Tutorial1 _tut1;

    public void SetTut(int level)
    {
        if (level == 1)
        {
            _tut1.StartTut();
        }
    }
}
