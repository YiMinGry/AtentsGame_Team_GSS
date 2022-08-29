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

    public int idx;//ȸ����ȣ
    public string ssID;//���Ǿ��̵�
    public string ID;//����̽� ���̵�
    public string nickName;//�г���
    public long coin1;//�Ϲ���ȭ
    public long coin2;//Ư����ȭ



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

        //NetManager.instance.AddRollingMSG($"ȯ���մϴ�, {nickName}��.");

        //����
        //bl_SceneLoaderManager.LoadScene("Main_Lobby");

        //�׽�Ʈ�� Dev_Lobby ������ �ʿ��ϸ� �� ���ηκ� �κ� �ּ��ϰ� �Ʒ� ����κ� �ּ� Ǯ��

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
