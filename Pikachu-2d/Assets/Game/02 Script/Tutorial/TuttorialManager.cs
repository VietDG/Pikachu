using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuttorialManager : MonoBehaviour
{
    [SerializeField] Tutorial1 _tut1;
    [SerializeField] Tutorial2 _tut2;

    public void SetTut(int level)
    {
        if (level == 1)
        {
            _tut1.StartTut();
        }
        if (level == 2)
        {
            _tut2.StartTut();
        }
        GameController.Instance.uiGamePlayManager.SetMask(true);
    }
}
