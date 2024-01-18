using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

public static class DateTimeUtility
{
    public static void ToMinuteSecond(StringBuilder sb, int totalSecond)
    {
        sb.Clear();

        int minute = (int)(totalSecond / 60);// phút
        int second = (int)(totalSecond - minute * 60);// giây

        if (minute != 0)
        {
            if (minute < 10)
                sb.Append('0');

            sb.Append(minute);
            sb.Append(':');
        }
        else
        {
            sb.Append('0');
            sb.Append('0');
            sb.Append(':');
        }

        if (second != 0)
        {
            if (second < 10)
                sb.Append('0');

            sb.Append(second);
        }
        else
        {
            sb.Append('0');
            sb.Append('0');
        }
    }
}
