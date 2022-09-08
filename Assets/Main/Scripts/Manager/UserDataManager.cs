using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public struct MGPlayData//�̴ϰ��� ���� üũ��
{
    public bool _is1st;//1���� ����� �ִ���
    public int _maxScore;//�ִ� ����
    public int _playCount;//�÷��� ī��Ʈ

}

public class UserDataManager : MonoSingleton<UserDataManager>
{
    [SerializeField]
    private KoreaInput koreaInput;

    public int idx;//ȸ����ȣ
    public string ssID;//���Ǿ��̵�
    public string ID;//����̽� ���̵�
    public string nickName;//�г���
    public long coin1;//�Ϲ���ȭ
    public long coin2;//Ư����ȭ
    public string mfList;
    public string archiveList;//���� ����Ʈ

    //�̴ϰ��� ������ ������ ���ٿ�
    public MGPlayData MG1PlayData;
    public MGPlayData MG2PlayData;
    public MGPlayData MG3PlayData;
    public MGPlayData MG4PlayData;
    public MGPlayData MG5PlayData;


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        NetEventManager.Regist("LoginOK", S2CL_LoginOK);
        NetEventManager.Regist("SetUserNickName", S2CL_SetUserNickName);
        NetEventManager.Regist("UserCoinUpdate", S2CL_UserCoinUpdate);

        NetEventManager.Regist("ReadMyAllRanking", S2CL_ReadMyAllRanking);
        NetEventManager.Regist("ArchiveUpdate", S2CL_ArchiveUpdate);

        ID = SystemInfo.deviceUniqueIdentifier;
    }

    public void RefreshUserInfo()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "RefreshUserInfo");
        _userData.Add("ID", UserDataManager.instance.ID);

        NetManager.instance.CL2S_SEND(_userData);
    }

    public void S2CL_LoginOK(JObject _jdata)
    {

        idx = int.Parse(_jdata["idx"].ToString());
        ssID = _jdata["ssID"].ToString();
        ID = _jdata["ID"].ToString();
        nickName = _jdata["nickName"].ToString();
        coin1 = long.Parse(_jdata["coin1"].ToString());
        coin2 = long.Parse(_jdata["coin2"].ToString());
        mfList = _jdata["MFList"].ToString();
        archiveList = _jdata["ArchiveList"].ToString();
        JObject _list = new JObject();
        _list = JObject.Parse(mfList);

        MFDataManager.instance.SetAllMF(_list);


        JArray _mgarr = JArray.Parse(_jdata["MGData"].ToString());


        MG1PlayData._is1st = _mgarr[0]["_is1st"].ToString().Equals("true");
        MG1PlayData._playCount = int.Parse(_mgarr[0]["_playCount"].ToString());
        MG1PlayData._maxScore = int.Parse(_mgarr[0]["_maxScore"].ToString());


        MG2PlayData._is1st = _mgarr[1]["_is1st"].ToString().Equals("true");
        MG2PlayData._playCount = int.Parse(_mgarr[1]["_playCount"].ToString());
        MG2PlayData._maxScore = int.Parse(_mgarr[1]["_maxScore"].ToString());

        MG3PlayData._is1st = _mgarr[2]["_is1st"].ToString().Equals("true");
        MG3PlayData._playCount = int.Parse(_mgarr[2]["_playCount"].ToString());
        MG3PlayData._maxScore = int.Parse(_mgarr[2]["_maxScore"].ToString());

        MG4PlayData._is1st = _mgarr[3]["_is1st"].ToString().Equals("true");
        MG4PlayData._playCount = int.Parse(_mgarr[3]["_playCount"].ToString());
        MG4PlayData._maxScore = int.Parse(_mgarr[3]["_maxScore"].ToString());

        MG5PlayData._is1st = _mgarr[4]["_is1st"].ToString().Equals("true");
        MG5PlayData._playCount = int.Parse(_mgarr[4]["_playCount"].ToString());
        MG5PlayData._maxScore = int.Parse(_mgarr[4]["_maxScore"].ToString());


        if (!_jdata["retMsg"].ToString().Equals("Refresh"))
        {
            NetManager.instance.AddRollingMSG("로그인", $"{nickName}님 환영합니다.");
            NetManager.instance.AddRollingMSG("레트로 프렌즈", $"{nickName}님 즐거운 시간 되십시오.");

            //����
            bl_SceneLoaderManager.LoadScene("Main_Lobby");

            //�׽�Ʈ�� Dev_Lobby ������ �ʿ��ϸ� �� ���ηκ� �κ� �ּ��ϰ� �Ʒ� ����κ� �ּ� Ǯ��

            //bl_SceneLoaderManager.LoadScene("Dev_Lobby");
        }
    }

    public void S2CL_SetUserNickName(JObject _jdata)
    {
        NickNamePop();
    }

    void NickNamePop()
    {
        koreaInput.gameObject.SetActive(true);
    }


    void NickNamePopClose()
    {
        koreaInput.Clear();
        koreaInput.gameObject.SetActive(false);
    }

    public void CL2S_SetUserNickName()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "SetUserNickName");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("nickName", koreaInput.nickText.text);

        NetManager.instance.CL2S_SEND(_userData);

        NickNamePopClose();
    }

    public bool CL2S_UserCoinUpdate(int _coinType, int _addAmount)//0�Ϲ���ȭ 1Ư����ȭ / �����ٰ� ������ ��� ���Ҵ� ����
    {

        long _money = _coinType == 0 ? coin1 : coin2;

        if (_money + _addAmount < 0)
        {

            //������ ����
            return false;
        }
        else
        {
            JObject _userData = new JObject();
            _userData.Add("cmd", "UserCoinUpdate");
            _userData.Add("ID", UserDataManager.instance.ID);
            _userData.Add("CoinIdx", "coin" + (_coinType + 1));
            _userData.Add("Amount", _money + _addAmount);

            NetManager.instance.CL2S_SEND(_userData);

            return true;
        }
    }

    public void S2CL_UserCoinUpdate(JObject _jdata)
    {
        coin1 = long.Parse(_jdata["coin1"].ToString());
        coin2 = long.Parse(_jdata["coin2"].ToString());
    }

    public void CL2S_ReadMyAllRanking()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "ReadMyAllRanking");
        _userData.Add("ID", UserDataManager.instance.ID);

        NetManager.instance.CL2S_SEND(_userData);
    }

    public void S2CL_ReadMyAllRanking(JObject _jdata)
    {
        Debug.Log(_jdata.ToString());

        JArray _data = JArray.Parse(_jdata["MG_1"].ToString());
        string _str = _data[0].ToString();

        Debug.Log(_str);
    }

    //���� �޼��ߴٰ� ������ �����ϴ� �Լ� _archiveName�� ���ϴ� ���� ���̺� �̸� �־ �Լ� ȣ��
    public void CL2S_ArchiveUpdate(string _archiveName)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "ArchiveUpdate");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("ArchiveName", _archiveName);


        NetManager.instance.CL2S_SEND(_userData);
    }

    //���� �޼��ϰ� �������� �����ִ� ���� ���� ����Ʈ ������
    public void S2CL_ArchiveUpdate(JObject _jdata)
    {
        Debug.Log(_jdata.ToString());

        archiveList = _jdata["ArchiveList"].ToString();
    }
}
