using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Ranking : MonoBehaviour
{
    private void Awake()
    {
        NetEventManager.Regist("LoginOK", S2CL_UpdateRanking);
    }

    public void CL2S_UpdateRanking() 
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "UpdateRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("nickName", UserDataManager.instance.nickName);
        _userData.Add("MG_NAME", "mg1rank");
        _userData.Add("Score", "2000");

        NetManager.instance.CL2S_SEND(_userData.ToString());
    }

    public void S2CL_UpdateRanking(JObject _jdata) 
    {
        Debug.Log(_jdata.ToString());
    }
}
