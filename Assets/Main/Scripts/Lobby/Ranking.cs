using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    Text[] rinfos_Num;
    [SerializeField]
    Text[] rinfos_Name;
    [SerializeField]
    Text[] rinfos_Score;

    private void Awake()
    {
        NetEventManager.Regist("ReadRanking", S2CL_ReadRanking);//서버에서 ReadRanking 커멘드로 패킷이 올경우 실행
        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);//서버에서 UpdateRanking 커멘드로 패킷이 올경우 실행
        NetEventManager.Regist("TotalRanking", S2CL_TotalRanking);
    }

    private void Start()
    {
        OnTotalRanking();
    }
    //종합 랭킹 10위+ 리스트를 서버에 요청하는 함수
    public void OnTotalRanking()
    {
        CL2S_TotalRanking();
    }
    //랭킹 10위+본인 의 리스트를 서버에 요청하는 함수
    public void OnReadRanking(string _name)
    {
        CL2S_ReadRanking(_name);
    }
    //서버에 내 접수를 업데이트하고 랭킹 정보를 요청하는 함수
    public void OnUpdateRanking(int _score)
    {
        CL2S_UpdateRanking(_score);
    }
    //랭킹 10위+본인 의 리스트를 서버에 요청하기위해 패킷 생성 후 전송 
    void CL2S_TotalRanking()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "TotalRanking");
        _userData.Add("ID", UserDataManager.instance.ID);

        NetManager.instance.CL2S_SEND(_userData);
    }
    //랭킹 10위+본인 의 리스트를 서버에 요청하기위해 패킷 생성 후 전송 
    void CL2S_ReadRanking(string _name)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "ReadRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("MG_NAME", _name);

        NetManager.instance.CL2S_SEND(_userData);
    }

    //서버에 내 접수를 업데이트하고 랭킹 정보를 요청하는 패킷 생성 후 전송
    void CL2S_UpdateRanking(int _score)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "UpdateRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("nickName", UserDataManager.instance.nickName);
        _userData.Add("MG_NAME", "MG_1");
        _userData.Add("Score", _score);

        NetManager.instance.CL2S_SEND(_userData);
    }

    //랭킹 받아와서 ui에 그려줄 함수
    public void S2CL_UpdateRanking(JObject _jdata)
    {
    }
    //내 점수 업데이트하고 ui에 그려줄 함수
    public void S2CL_ReadRanking(JObject _jdata)
    {
        Debug.Log(_jdata);
        JArray _arr = JArray.Parse(_jdata["Top10"].ToString());

        for (int i = 0; i < _arr.Count; i++)
        {
            rinfos_Num[i].text = ($"{_arr[i]["Rank"]}등");
            rinfos_Name[i].text = ($"{_arr[i]["nickName"]}");
            rinfos_Score[i].text = ($"{_arr[i]["Score"]}점");
        }
    }
    public void S2CL_TotalRanking(JObject _jdata)
    {
        Debug.Log(_jdata);
        JArray _arr = JArray.Parse(_jdata["Top10"].ToString());

        for (int i = 0; i < _arr.Count; i++)
        {
            rinfos_Num[i].text = ($"{_arr[i]["MG_Total_Rank"]}등");
            rinfos_Name[i].text = ($"{_arr[i]["nickName"]}");
            rinfos_Score[i].text = ($"{_arr[i]["MG_Total_Score"]}점");
        }
    }

    private void OnDisable()
    {
        NetEventManager.UnRegist("ReadRanking", S2CL_ReadRanking);
        NetEventManager.UnRegist("UpdateRanking", S2CL_UpdateRanking);
        NetEventManager.UnRegist("TotalRanking", S2CL_TotalRanking);
    }
}
