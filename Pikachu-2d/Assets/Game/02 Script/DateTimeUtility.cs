using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

public static class DateTimeUtility
{
    public static void SetLastLoginTime(float time)
    {
        lastLoginTime = time;
    }

    public static void SetLoginTime(float time)
    {
        loginDeltaTime = time - lastLoginTime;
    }

    public static float GetDeltaTime(float remainingTime)
    {
        return Mathf.Max(remainingTime - loginDeltaTime - Time.realtimeSinceStartup);
    }

    private static float lastLoginTime;

    private static float loginDeltaTime;

    public static DateTime Get(string s)
    {
        DateTime.TryParse(s, out DateTime dateTime);
        return dateTime;
    }

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

    public static void ToHourMinuteSecond(StringBuilder sb, int totalSecond)
    {
        sb.Clear();

        int hour = (int)(totalSecond / 3600);
        int minute = (int)((totalSecond - hour * 3600) / 60);
        int second = (int)(totalSecond - hour * 3600 - minute * 60);

        if (hour != 0)
        {
            if (hour < 10)
                sb.Append('0');

            sb.Append(hour);
            sb.Append(':');
        }
        else
        {
            sb.Append('0');
            sb.Append('0');
            sb.Append(':');
        }

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

    public static void ToDayHour(StringBuilder sb, float totalSecond)
    {
        sb.Clear();

        int day = (int)(totalSecond / (3600 * 24));
        int hour = (int)((totalSecond - day * (3600 * 24)) / 3600);

        if (day != 0)
        {
            sb.Append(day);
            sb.Append('d');
            sb.Append(':');
        }
        else
        {
            sb.Append('0');
            sb.Append('0');
            sb.Append(':');
            sb.Append('d');
        }

        if (hour != 0)
        {
            if (hour < 10)
                sb.Append('0');

            sb.Append(hour);
            sb.Append('h');
        }
        else
        {
            sb.Append('0');
            sb.Append('0');
            sb.Append('h');
        }
    }
}
