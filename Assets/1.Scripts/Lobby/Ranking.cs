using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Ranking : MonoBehaviour
{
    private void Awake()
    {
        NetEventManager.Regist("ReadRanking", S2CL_ReadRanking);//서버에서 ReadRanking 커멘드로 패킷이 올경우 실행
        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);//서버에서 UpdateRanking 커멘드로 패킷이 올경우 실행
    }

    //랭킹 10위+본인 의 리스트를 서버에 요청하는 함수
    public void OnReadRanking() 
    {
        CL2S_ReadRanking();
    }
    //서버에 내 접수를 업데이트하고 랭킹 정보를 요청하는 함수
    public void OnUpdateRanking(int _score)
    {
        CL2S_UpdateRanking(_score);
    }

    //랭킹 10위+본인 의 리스트를 서버에 요청하기위해 패킷 생성 후 전송 
    void CL2S_ReadRanking()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "ReadRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("MG_NAME", "mg1rank");

        NetManager.instance.CL2S_SEND(_userData);
    }

    //서버에 내 접수를 업데이트하고 랭킹 정보를 요청하는 패킷 생성 후 전송
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

    //랭킹 받아와서 ui에 그려줄 함수
    public void S2CL_UpdateRanking(JObject _jdata)
    {
    }
    //내 점수 업데이트하고 ui에 그려줄 함수
    public void S2CL_ReadRanking(JObject _jdata)
    {
    }
}
