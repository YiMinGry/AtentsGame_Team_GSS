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
    /*�ʱ�ȭ �κ�*/
    //��ü ������Ʈ ���� ���� ����
    //��ü ������Ʈ ���� ���� ����
    //0:�̺��� 1:���� 2:����
    public void SetAllMF(JObject _list)
    {
        for (int i = 0; i < mfarr.Length; i++)
        {
            mfarr[i].isHave = int.Parse(_list["friend_" + i].ToString()) == 0 ? false : true;
            mfarr[i].isChoose = int.Parse(_list["friend_" + i].ToString()) == 2 ? true : false;

        }
    }

    /*��� �κ�*/
    //id�� Ư�� �̴�ģ�� ���� �ҷ�����

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

    //�������� �̴�ģ�� ���� ���� 3���� ����
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


    //id�� �������� �������ִ� ���(�̱��ؼ� ������)//�������� �ʿ�
    //id�� �������� �̴�ģ�� ������·� �������ִ� ���//���� ���� �ʿ�
    //id�� �������� �̴�ģ�� �����ϴ� ���//���� ���� �ʿ�
    //0:�̺��� 1:���� 2:����
    private void CL2S_ChangeMFState(int _idx, int _state)
    {
        JObject _data = new JObject();
        _data.Add("cmd", "MFStateChange");
        _data.Add("ID", UserDataManager.instance.ID);
        _data.Add("MFIdx", _idx);
        _data.Add("MFState", _state);

        NetManager.instance.CL2S_SEND(_data);
    }

    //�̴�ģ�� ���� ���� �Ϸ� ��Ŷ
    public void S2CL_MFStateChange(JObject _jdata)
    {
        Debug.Log(_jdata);
        FindMF(int.Parse(_jdata["MFIdx"].ToString())).isHave = int.Parse(_jdata["MFState"].ToString()) == 0 ? false : true;
        FindMF(int.Parse(_jdata["MFIdx"].ToString())).isChoose = int.Parse(_jdata["MFState"].ToString()) == 2 ? true : false;

        EventManager.Invoke("RefreshMF_" + _jdata["MFIdx"].ToString(), "");

        EventManager.Invoke("MF_Refresh", "");

    }

    public void Send_HaveMF(int _idx) //�̱�, ���� ������ �������� ���·� �����ϴ� �뵵
    {
        if (FindMF(_idx).isChoose == true || FindMF(_idx).isHave == false)//���������ʰų� �������϶��� �����ϰ�
        {
            CL2S_ChangeMFState(_idx, 1);
        }
    }
    public void Send_ChoiceMF(int _idx) //�̴�ģ�� ������ ���·� �����ϴ� �뵵
    {
        if (Ret3MF().Count < 3)
        {
            if (FindMF(_idx).isChoose == false && FindMF(_idx).isHave == true)//�����ϰ��ְ� �������� �ƴҶ�
            {
                CL2S_ChangeMFState(_idx, 2);
            }
        }
    }
}
