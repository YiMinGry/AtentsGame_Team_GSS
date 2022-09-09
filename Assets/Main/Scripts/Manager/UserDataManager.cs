using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public struct MGPlayData//미니게임 업적 체크용
{
    public bool _is1st;//1등한 기록이 있는지
    public int _maxScore;//최대 점수
    public int _playCount;//플레이 카운트

}

public class UserDataManager : MonoSingleton<UserDataManager>
{
    [SerializeField]
    private KoreaInput koreaInput;

    public int idx;//회원번호
    public string ssID;//세션아이디
    public string ID;//디바이스 아이디
    public string nickName;//닉네임
    public long coin1;//일반재화
    public long coin2;//특수재화
    public string mfList;
    public string archiveList;//업적 리스트

    //미니게임 업적용 데이터 접근용
    public MGPlayData MG1PlayData;
    public MGPlayData MG2PlayData;
    public MGPlayData MG3PlayData;
    public MGPlayData MG4PlayData;
    public MGPlayData MG5PlayData;

    //업적용 조건
    
    private int[,] conditionScore= { { 1, 5, 10, 25, 50 }, { 500,1500,3000,4500,6000 }, { 1, 5, 10, 25, 50 }, { 1, 5, 10, 25, 50 } };
    private int[,] conditionPlayCount= { { 1, 5, 10, 25, 50 }, { 1, 5, 10, 25, 50 }, { 1, 5, 10, 25, 50 }, { 1, 5, 10, 25, 50 } };
    private int[] conditionMfCount = {1,10,20,30};
    private int[] coin1Condition = {100,500,2000,5000,10000};
    private int[] coin2Condition = { 50, 200, 500, 1000,3000 };







    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        NetEventManager.Regist("LoginOK", S2CL_LoginOK);
        NetEventManager.Regist("SetUserNickName", S2CL_SetUserNickName);
        NetEventManager.Regist("UserCoinUpdate", S2CL_UserCoinUpdate);

        //NetEventManager.Regist("ReadMyAllRanking", S2CL_ReadMyAllRanking);
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

    //업적 달성했다고 서버로 전송하는 함수 _archiveName에 원하는 업적 테이블 이름 넣어서 함수 호출
    public void CL2S_ArchiveUpdate(string _archiveName)
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "ArchiveUpdate");
        _userData.Add("ID", UserDataManager.instance.ID);
        _userData.Add("ArchiveName", _archiveName);


        NetManager.instance.CL2S_SEND(_userData);

        NetManager.instance.AddRollingMSG("업적", $"{_archiveName}업적을 달성했습니다.");

    }

    //업적 달성하고 서버에서 보내주는 답장 업적 리스트 보여줌
    public void S2CL_ArchiveUpdate(JObject _jdata)
    {
        Debug.Log(_jdata.ToString());

        archiveList = _jdata["ArchiveList"].ToString();
    }
}
