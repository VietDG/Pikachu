using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoadLevelFormData
{
    public int shapeid;
    public int time;
    public int kind;
    public int score;
    public int boom;
    public int hammer = 1;

    public int repeat;
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public int movenum;
}
