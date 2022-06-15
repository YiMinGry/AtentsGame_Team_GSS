using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Ranking : MonoBehaviour
{
    private void Awake()
    {
        NetEventManager.Regist("ReadRanking", S2CL_ReadRanking);
        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);
    }

    public void OnReadRanking() 
    {
        CL2S_ReadRanking();
    }
    public void OnUpdateRanking(int _score)
    {
        CL2S_UpdateRanking(_score);
    }

    void CL2S_ReadRanking()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "ReadRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("MG_NAME", "mg1rank");

        NetManager.instance.CL2S_SEND(_userData);
    }

    void CL2S_UpdateRanking(int _score)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "UpdateRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("nickName", UserDataManager.instance.nickName);
        _userData.Add("MG_NAME", "mg1rank");
        _userData.Add("Score", _score);

        NetManager.instance.CL2S_SEND(_userData);
    }

    public void S2CL_UpdateRanking(JObject _jdata)
    {
    }
    public void S2CL_ReadRanking(JObject _jdata)
    {
    }
}
