using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class UitilyTime
{
    public static void SetMinuteAndSencond(StringBuilder stringBuilder, int senCond)
    {
        stringBuilder.Clear();

        int m = (int)(senCond / 60);
        int s = (int)(senCond - m * 60);

        if (m != 0)
        {
            if (m < 10)
                stringBuilder.Append('0');

            stringBuilder.Append(m);
            stringBuilder.Append(':');
        }
        else
        {
            stringBuilder.Append('0');
            stringBuilder.Append('0');
            stringBuilder.Append(':');
        }

        if (s != 0)
        {
            if (s < 10)
                stringBuilder.Append('0');

            stringBuilder.Append(s);
        }
        else
        {
            stringBuilder.Append('0');
            stringBuilder.Append('0');
        }
    }
}
