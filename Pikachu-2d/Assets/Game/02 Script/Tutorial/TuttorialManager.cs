using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuttorialManager : SingletonMonoBehaviour<TuttorialManager>
{
    public Tutorial1 _tut1;
    public Tutorial2 _tut2;
    public Tutorial3 _tut3;
    public Tutorial4 _tut4;
    public Tutorial5 _tut5;

    private int level;
    public void SetTut(int level)
    {
        this.level = level;

        FunctionCommon.DelayTime(1f, () =>
        {
            InitLevelTut();
        });
    }

    private void InitLevelTut()
    {
        switch (level)
        {
            case 1:
                _tut1.StartTut();
                break;
            case 2:
                _tut2.StartTut();
                break;
            case 3:
                _tut3.StartTut();
                break;
            case 4:
                _tut4.StartTut();
                break;
            case 5:
                _tut5.StartTut();
                break;
            default:
                break;
        }
    }
}
