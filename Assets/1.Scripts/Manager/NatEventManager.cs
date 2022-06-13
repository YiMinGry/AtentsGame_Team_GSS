using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public delegate void CallbackInJdata(JObject _jdata);

public static class NatEventManager
{
    private static Dictionary<string, List<CallbackInJdata>> _keyPair = new Dictionary<string, List<CallbackInJdata>>();

    public static bool Regist(string key, CallbackInJdata func)
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
            _keyPair.Add(key, new List<CallbackInJdata>());
            _keyPair[key].Add(func);
        }

        return true;
    }

    public static bool UnRegist(string key, CallbackInJdata func)
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

    public static bool Invoke(string key, JObject _jdata)
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
                func.Invoke(_jdata);
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
