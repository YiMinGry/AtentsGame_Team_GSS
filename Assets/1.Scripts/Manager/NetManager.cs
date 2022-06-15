using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System;
using System.Threading;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class NetManager : MonoSingleton<NetManager>
{
    //private string IP = "192.168.219.114";
    private string IP = "localhost";

    private string PORT = "5641";
    private string SERVICE_NAME = "/MGServer";

    public WebSocket m_Socket = null;
    public bool isConnect = false;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            m_Socket = new WebSocket("ws://" + IP + ":" + PORT + SERVICE_NAME);
            m_Socket.OnMessage += S2CL_RECV;

            m_Socket.OnOpen += (sender, e) =>
            {
                JObject json = new JObject();
                json.Add("cmd", "ssEnter");
                json.Add("ID", SystemInfo.deviceUniqueIdentifier);
                CL2S_SEND(json);
            };

            m_Socket.OnClose += CloseConnect;
        }
        catch
        {
        }

        Connect();
    }


    public void Connect()
    {
        try
        {
            if (m_Socket == null || !m_Socket.IsAlive)
            {
                m_Socket.Connect();
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void CloseConnect(object sender, CloseEventArgs e)
    {
        DisconncectServer();
    }
    public void CL2S_SEND(JObject msg)
    {
        if (!m_Socket.IsAlive)
        {
            return;
        }
        try
        {
            Debug.Log($"CL2S_SEND {msg["cmd"].ToString()}: " + msg);

            m_Socket.Send(msg.ToString());
        }
        catch (Exception)
        {

            throw;
        }

    }
    public void S2CL_RECV(object sender, MessageEventArgs e)
    {
        JObject msg = JObject.Parse(e.Data);
        Debug.Log($"S2CL_RECV {msg["cmd"].ToString()}: " + e.Data);

        NetEventManager.Invoke(msg["cmd"].ToString(), msg);
    }

    public void DisconncectServer()
    {
        try
        {
            if (m_Socket == null)
                return;

            if (m_Socket.IsAlive)
                m_Socket.Close();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void Update()
    {
        isConnect = m_Socket.IsAlive;

        //if (!m_Socket.IsAlive) 
        //{
        //    Debug.LogWarning("서버 접속 실패");
        //}
    }

    private void OnApplicationQuit()
    {
        DisconncectServer();
    }
}
