using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

public class Chat : MonoBehaviour
{
    public ScrollRect chatScroll;
    public InputField chatField;
    public Text chatText;

    private void Awake()
    {
        NetEventManager.Regist("Chat", S2CL_UpdateChat);
    }

    public void SendText()
    {
        CL2S_UpdateRanking();
    }

    void CL2S_UpdateRanking()
    {
        JObject _chatData = new JObject();
        _chatData.Add("cmd", "Chat");
        _chatData.Add("ID", UserDataManager.instance.ID);
        _chatData.Add("nickName", UserDataManager.instance.nickName);
        _chatData.Add("msg", chatField.text);

        NetManager.instance.CL2S_SEND(_chatData);
    }

    public void S2CL_UpdateChat(JObject _jdata)
    {
        Debug.Log(_jdata.ToString());

        string _id = _jdata["ID"].ToString();
        string _msg = _jdata["msg"].ToString();

        chatText.text = chatText.text + _id + " : " + _msg + "\n";
    }
}

