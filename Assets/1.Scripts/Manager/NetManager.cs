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
                json.Add("cmd", "userEnter");
                json.Add("ID", "1");
                json.Add("nickName", "user");

                CL2S_SEND(json.ToString());
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
    public void CL2S_SEND(string msg)
    {
        if (!m_Socket.IsAlive)
        {
            return;
        }
        try
        {
            m_Socket.Send(msg);
        }
        catch (Exception)
        {

            throw;
        }

    }
    public void S2CL_RECV(object sender, MessageEventArgs e)
    {
        //string 데이터
        Debug.Log(e.Data);

        //bytes 데이터
        Debug.Log(e.RawData);
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

    private void OnApplicationQuit()
    {
        DisconncectServer();
    }
}
