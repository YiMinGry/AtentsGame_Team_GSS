using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Globalization;
using System.Linq;

public static class Utill
{
    public static readonly string ServerUrl = "";

    //시간 관련
    private static readonly System.DateTime Jan1st1970 = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

    public static System.DateTime ConvertSeverTime(string serverTime)
    {
        CultureInfo provider = CultureInfo.InvariantCulture;
        return System.DateTime.ParseExact(serverTime, "yyyyMMddHHmmss", provider);
    }
    public static long CurrentTime()
    {
        return long.Parse(System.DateTime.Now.AddHours(-9).ToString("yyyyMMddHHmmss"));
    }
    public static long ConvertTimeSeconds(System.DateTime dateTime)
    {
        return (long)(dateTime - Jan1st1970).TotalSeconds;
    }
    public static long CurrentTimeSeconds()
    {
        return (long)(System.DateTime.UtcNow.AddHours(-9) - Jan1st1970).TotalSeconds;
    }


    //숫자에 콤마 찍어주는 기능
    public static string ConvertNumberComma(long inputnumber)
    {
        string unit = String.Format("{0:#,0}", inputnumber);
        return unit;
    }
    public static string ConvertNumber(long inputnumber)
    {//123T1231B
        string sign = string.Empty;
        long number = inputnumber;
        if (inputnumber < 0)
        {
            sign = "-";
        }

        int max = (int)Math.Abs(number / 1000000000000);
        int billion = (int)Math.Abs((number % 1000000000000) / 100000000);
        int millon = (int)Math.Abs((number % 100000000) / 10000);

        string trillionstring = string.Format("{0}T", max);

        string billionstring = string.Format("{0}B", billion.ToString());

        string millonstring = string.Format("{0}M", millon.ToString());

        string numberstring = Math.Abs((int)(number % 10000)).ToString();
        if (Math.Abs(number) > 0 && Math.Abs((number % 10000)) <= 0)
        {
            numberstring = string.Empty;
        }

        if (max <= 0)
        {
            trillionstring = string.Empty;
        }
        else
        {
            millonstring = string.Empty;
            numberstring = string.Empty;
        }

        if (billion <= 0)
        {
            billionstring = string.Empty;
        }
        else
        {
            numberstring = string.Empty;
        }

        if (millon <= 0)
        {
            millonstring = string.Empty;
        }

        string unit = string.Format("{4}{3}{2}{1}{0}", numberstring, millonstring, billionstring, trillionstring, sign);
        return unit;
    }
    public static string ConvertNumberUnit(long inputnumber)
    {//123T1231B2312M3123
        string sign = string.Empty;
        long number = inputnumber;
        if (inputnumber < 0)
        {
            sign = "-";
        }

        int max = (int)Math.Abs(number / 1000000000000);
        int billion = (int)Math.Abs((number % 1000000000000) / 100000000);
        int millon = (int)Math.Abs((number % 100000000) / 10000);

        string trillionstring = string.Format("{0}T", max);

        if (max <= 0)
        {
            trillionstring = string.Empty;
        }

        string billionstring = string.Format("{0}B", billion.ToString());

        if (billion <= 0)
        {
            billionstring = string.Empty;
        }

        string millonstring = string.Format("{0}M", millon.ToString());
        if (millon <= 0)
        {
            millonstring = string.Empty;
        }

        string numberstring = Math.Abs((int)(number % 10000)).ToString();
        if (Math.Abs(number) > 0 && Math.Abs((number % 10000)) <= 0)
        {
            numberstring = string.Empty;
        }

        string unit = string.Format("{4}{3}{2}{1}{0}", numberstring, millonstring, billionstring, trillionstring, sign);

        return unit;
    }
    public static string ConvertNumberShortUnit(long number)
    {//123.1T
        int max = (int)(number / 1000000000000);
        int billion = (int)((number % 1000000000000) / 100000000);
        int millon = (int)((number % 100000000) / 10000);

        string trillionstring = string.Format("{0}T", (Math.Truncate((number * 0.0000000001) / 10) / 10).ToString("0.#"));

        if (max <= 0)
        {
            trillionstring = string.Empty;
        }
        else
        {
            return string.Format("{0}", trillionstring);
        }

        string billionstring = string.Format("{0}B", ((Math.Truncate(((number % 1000000000000) * 0.000001) / 10) / 10).ToString("0.#")));


        if (billion <= 0)
        {
            billionstring = string.Empty;
        }
        else
        {
            return string.Format("{0}", billionstring);
        }

        string numformat = "D4";
        string millonstring = string.Format("{0}M", ((Math.Truncate(((number % 100000000) * 0.01) / 10) / 10).ToString("0.#")));
        if (millon <= 0)
        {
            numformat = string.Empty;
            millonstring = string.Empty;
        }
        else
        {
            return string.Format("{0}", millonstring);
        }

        string numberstring = (number % 10000).ToString(numformat);
        if (number > 0 && (number % 10000 <= 0))
        {
            numberstring = string.Empty;
        }

        string unit = string.Format("{0}", numberstring);

        return unit;
    }

    //문자열 정규식
    public static bool CheckSpecialText(string txt)
    {
        string str = @"[^0-9a-zA-Z가-힣\s]";
        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
        return rex.IsMatch(txt);
    }

    //코루틴 재활용
    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float, WaitForSeconds> _timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!_timeInterval.TryGetValue(seconds, out wfs))
        {
            _timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
        }
        return wfs;
    }
}