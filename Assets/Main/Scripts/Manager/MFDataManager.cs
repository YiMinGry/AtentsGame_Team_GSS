using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MFDataManager : MonoSingleton<MFDataManager>
{
    public MiniFriendData[] mfarr;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        NetEventManager.Regist("MFStateChange", S2CL_MFStateChange);
    }
    /*초기화 부분*/
    //전체 오브젝트 소유 여부 세팅
    //잔체 오브젝트 착용 여부 세팅
    //0:미보유 1:보유 2:착용
    public void SetAllMF(JObject _list)
    {
        for (int i = 0; i < mfarr.Length; i++)
        {
            mfarr[i].isHave = int.Parse(_list["friend_" + i].ToString()) == 0 ? false : true;
            mfarr[i].isChoose = int.Parse(_list["friend_" + i].ToString()) == 2 ? true : false;

        }
    }

    /*기능 부분*/
    //id로 특정 미니친구 정보 불러오기

    public MiniFriendData FindMF(int _idx)
    {
        for (int i = 0; i < mfarr.Length; i++)
        {
            if (mfarr[i].id.Equals(_idx))
            {
                return mfarr[i];
            }
        }

        return null;
    }

    //착용중인 미니친구 갯수 리턴 3마리 기준
    public List<MiniFriendData> Ret3MF()
    {
        List<MiniFriendData> retList = new List<MiniFriendData>();

        for (int i = 0; i < mfarr.Length; i++)
        {
            if (mfarr[i].isChoose == true)
            {
                retList.Add(mfarr[i]);

                if (retList.Count >= 3)
                {
                    break;
                }
            }
        }

        return retList;
    }


    //id로 소유여부 변경해주는 기능(뽑기해서 얻을때)//서버연동 필요
    //id로 소유중인 미니친구 착용상태로 변경해주는 기능//서버 연동 필요
    //id로 착용중인 미니친구 해제하는 기능//서버 연동 필요
    //0:미보유 1:보유 2:착용
    private void CL2S_ChangeMFState(int _idx, int _state)
    {
        JObject _data = new JObject();
        _data.Add("cmd", "MFStateChange");
        _data.Add("ID", UserDataManager.instance.ID);
        _data.Add("MFIdx", _idx);
        _data.Add("MFState", _state);

        NetManager.instance.CL2S_SEND(_data);
    }

    //미니친구 상태 변경 완료 패킷
    public void S2CL_MFStateChange(JObject _jdata)
    {
        Debug.Log(_jdata);
        FindMF(int.Parse(_jdata["MFIdx"].ToString())).isHave = int.Parse(_jdata["MFState"].ToString()) == 0 ? false : true;
        FindMF(int.Parse(_jdata["MFIdx"].ToString())).isChoose = int.Parse(_jdata["MFState"].ToString()) == 2 ? true : false;

        EventManager.Invoke("RefreshMF_" + _jdata["MFIdx"].ToString(), "");

        EventManager.Invoke("MF_Refresh", "");

    }

    public void Send_HaveMF(int _idx) //뽑기, 착용 해제로 소유중인 상태로 변경하는 용도
    {
        if (FindMF(_idx).isChoose == true || FindMF(_idx).isHave == false)//보유하지않거나 착용중일때만 실행하게
        {
            CL2S_ChangeMFState(_idx, 1);
        }
    }
    public void Send_ChoiceMF(int _idx) //미니친구 착용한 상태로 변경하는 용도
    {
        if (Ret3MF().Count < 3)
        {
            if (FindMF(_idx).isChoose == false && FindMF(_idx).isHave == true)//보유하고있고 착용중이 아닐때
            {
                CL2S_ChangeMFState(_idx, 2);
            }
        }
    }
}
