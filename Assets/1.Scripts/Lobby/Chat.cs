using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

public class Chat : MonoBehaviour
{
    public ScrollRect chatScroll;//채팅창 스크롤
    public InputField chatField;//내가 입력할 텍스트필드
    public Text chatText;//채팅창 메세지 나열할 텍스트

    private void Awake()
    {
        NetEventManager.Regist("Chat", S2CL_UpdateChat);//서버에서 Chat이라는 커멘드로 패킷이 올경우 실행
    }
    //채팅 보내는 함수
    public void SendText()
    {
        CL2S_UpdateRanking();
    }
    //클라에서 서버로 채팅 보내는 패킷
    void CL2S_UpdateRanking()
    {
        JObject _chatData = new JObject();
        _chatData.Add("cmd", "Chat");
        _chatData.Add("ID", UserDataManager.instance.ID);
        _chatData.Add("nickName", UserDataManager.instance.nickName);
        _chatData.Add("msg", chatField.text);

        NetManager.instance.CL2S_SEND(_chatData);
    }
    //서버에서 받은 Chat패킷으로 자동실행될 함수
    public void S2CL_UpdateChat(JObject _jdata)
    {
        Debug.Log(_jdata.ToString());

        string _id = _jdata["ID"].ToString();//닉네임으 넣어줘야하는데 아직 내용없어서 아이디로
        string _msg = _jdata["msg"].ToString();

        chatText.text = chatText.text + _id + " : " + _msg + "\n";
    }
}

