using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void Callback(string _str);

public static class EventManager
{
    private static Dictionary<string, List<Callback>> _keyPair = new Dictionary<string, List<Callback>>();

    public static bool Regist(string key, Callback func)
    {
        Debug.Log("Regist Key : " + key);
        if (_keyPair.ContainsKey(key) == true)
        {
            if (_keyPair[key].Contains(func) == false)
            {
                _keyPair[key].Add(func);
            }
        }
        else
        {
            _keyPair.Add(key, new List<Callback>());
            _keyPair[key].Add(func);
        }

        return true;
    }

    public static bool UnRegist(string key, Callback func)
    {
        if (_keyPair.ContainsKey(key) == true)
        {
            if (_keyPair[key].Contains(func) == true)
            {
                _keyPair[key].Remove(func);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    public static bool Invoke(string key, string _str)
    {
        if (_keyPair.ContainsKey(key) == false)
        {
            return false;
        }

        for (int nCount = 0; nCount < _keyPair[key].Count; ++nCount)
        {
            var func = _keyPair[key][nCount];
            if (func == null)
            {
                nCount = -1;
                continue;
            }

            try
            {
                func.Invoke(_str);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Exception : " + e);
            }

        }

        return true;
    }

    public static bool ClearRegiest()
    {
        _keyPair.Clear();
        return true;
    }

}
