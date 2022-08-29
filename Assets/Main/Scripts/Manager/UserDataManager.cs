using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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



    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        NetEventManager.Regist("LoginOK", S2CL_LoginOK);
        NetEventManager.Regist("SetUserNickName", S2CL_SetUserNickName);

        ID = SystemInfo.deviceUniqueIdentifier;
    }

    public void S2CL_LoginOK(JObject _jdata)
    {

        idx = int.Parse(_jdata["idx"].ToString());
        ssID = _jdata["ssID"].ToString();
        ID = _jdata["ID"].ToString();
        nickName = _jdata["nickName"].ToString();
        coin1 = long.Parse(_jdata["coin1"].ToString());
        coin2 = long.Parse(_jdata["coin1"].ToString());

        //NetManager.instance.AddRollingMSG($"환영합니다, {nickName}님.");

        //메인
        //bl_SceneLoaderManager.LoadScene("Main_Lobby");

        //테스트용 Dev_Lobby 진입이 필요하면 위 메인로비 부분 주석하고 아래 데브로비 주석 풀기

        bl_SceneLoaderManager.LoadScene("Dev_Lobby");
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
}
