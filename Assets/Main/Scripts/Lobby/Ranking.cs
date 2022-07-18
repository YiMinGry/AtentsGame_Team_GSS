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
        NetEventManager.Regist("ReadRanking", S2CL_ReadRanking);//�������� ReadRanking Ŀ���� ��Ŷ�� �ð�� ����
        NetEventManager.Regist("UpdateRanking", S2CL_UpdateRanking);//�������� UpdateRanking Ŀ���� ��Ŷ�� �ð�� ����
        NetEventManager.Regist("TotalRanking", S2CL_TotalRanking);
    }

    private void Start()
    {
        OnTotalRanking();
    }
    //���� ��ŷ 10��+ ����Ʈ�� ������ ��û�ϴ� �Լ�
    public void OnTotalRanking()
    {
        CL2S_TotalRanking();
    }
    //��ŷ 10��+���� �� ����Ʈ�� ������ ��û�ϴ� �Լ�
    public void OnReadRanking(string _name)
    {
        CL2S_ReadRanking(_name);
    }
    //������ �� ������ ������Ʈ�ϰ� ��ŷ ������ ��û�ϴ� �Լ�
    public void OnUpdateRanking(int _score)
    {
        CL2S_UpdateRanking(_score);
    }
    //��ŷ 10��+���� �� ����Ʈ�� ������ ��û�ϱ����� ��Ŷ ���� �� ���� 
    void CL2S_TotalRanking()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "TotalRanking");
        _userData.Add("ID", UserDataManager.instance.ID);

        NetManager.instance.CL2S_SEND(_userData);
    }
    //��ŷ 10��+���� �� ����Ʈ�� ������ ��û�ϱ����� ��Ŷ ���� �� ���� 
    void CL2S_ReadRanking(string _name)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "ReadRanking");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("MG_NAME", _name);

        NetManager.instance.CL2S_SEND(_userData);
    }

    //������ �� ������ ������Ʈ�ϰ� ��ŷ ������ ��û�ϴ� ��Ŷ ���� �� ����
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

    //��ŷ �޾ƿͼ� ui�� �׷��� �Լ�
    public void S2CL_UpdateRanking(JObject _jdata)
    {
    }
    //�� ���� ������Ʈ�ϰ� ui�� �׷��� �Լ�
    public void S2CL_ReadRanking(JObject _jdata)
    {
        Debug.Log(_jdata);
        JArray _arr = JArray.Parse(_jdata["Top10"].ToString());

        for (int i = 0; i < _arr.Count; i++)
        {
            rinfos_Num[i].text = ($"{_arr[i]["Rank"]}��");
            rinfos_Name[i].text = ($"{_arr[i]["nickName"]}");
            rinfos_Score[i].text = ($"{_arr[i]["Score"]}��");
        }
    }
    public void S2CL_TotalRanking(JObject _jdata)
    {
        Debug.Log(_jdata);
        JArray _arr = JArray.Parse(_jdata["Top10"].ToString());

        for (int i = 0; i < _arr.Count; i++)
        {
            rinfos_Num[i].text = ($"{_arr[i]["MG_Total_Rank"]}��");
            rinfos_Name[i].text = ($"{_arr[i]["nickName"]}");
            rinfos_Score[i].text = ($"{_arr[i]["MG_Total_Score"]}��");
        }
    }

    private void OnDisable()
    {
        NetEventManager.UnRegist("ReadRanking", S2CL_ReadRanking);
        NetEventManager.UnRegist("UpdateRanking", S2CL_UpdateRanking);
        NetEventManager.UnRegist("TotalRanking", S2CL_TotalRanking);
    }
}
