using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class UserDataManager : MonoSingleton<UserDataManager>
{
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

        NatEventManager.Regist("LoginOK", S2CL_LoginOK);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void S2CL_LoginOK(JObject _jdata)
    {

        idx = int.Parse(_jdata["idx"].ToString());
        ssID = _jdata["ssID"].ToString();
        ID = _jdata["ID"].ToString();
        nickName = _jdata["nickName"].ToString();
        coin1 = long.Parse(_jdata["coin1"].ToString());
        coin2 = long.Parse(_jdata["coin1"].ToString());

    }
}
