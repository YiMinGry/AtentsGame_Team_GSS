using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DG.Tweening;
using System.Collections;
using System.Net.Sockets;
using System.IO;

public class NetManager : MonoSingleton<NetManager>
{

    //����------------------------------
    // �⺻ ȣ��Ʈ/ ��Ʈ��ȣ
    //string ip = "localhost";
    string ip = "125.176.151.114";
    int port = 5641;

    public bool isConnect = false;
    [SerializeField]
    private GameObject RollingCavans;
    [SerializeField]
    private Text RollingText;

    bool socketReady;
    TcpClient socket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        ConnectToServer();
    }

    public void ConnectToServer()
    {
        // �̹� ����Ǿ��ٸ� �Լ� ����
        if (socketReady) return;

        // ���� ����
        try
        {
            if (SystemInfo.deviceUniqueIdentifier == "39e35f22fd9aebfc2bb8973e7a925ba0ffe77760")
            {
                socket = new TcpClient("localhost", port);

            }
            else 
            {
                socket = new TcpClient(ip, port);
            }

            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;

            JObject json = new JObject();
            json.Add("cmd", "ssEnter");
            json.Add("ID", SystemInfo.deviceUniqueIdentifier);
            CL2S_SEND(json);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void Update()
    {
        if (socketReady && stream.DataAvailable)
        {
            string data = reader.ReadLine();
            if (data != null)
            {
                OnMessage(data);
            }
        }
    }

    void OnMessage(string data)
    {
        JObject msg = JObject.Parse(data);
        Debug.Log($"S2CL_RECV {msg["cmd"].ToString()}: " + data);

        NetEventManager.Invoke(msg["cmd"].ToString(), msg);
    }

    void Send(string data)
    {
        if (!socketReady)
        {
            return;
        }
        string str = data;
        str = str.Replace("\r\n", "");

        writer.WriteLine(str);
        writer.Flush();
    }
    public void CL2S_SEND(JObject msg)
    {
        try
        {
            Debug.Log($"CL2S_SEND {msg["cmd"].ToString()}: " + msg);
            Send(msg.ToString());
        }
        catch (Exception)
        {

            throw;
        }

    }
    void OnApplicationQuit()
    {
        CloseSocket();
    }

    void CloseSocket()
    {
        if (!socketReady) return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }




    //�Ϲ�-------------------------------------------------------------------

    //�Ѹ� ������ �޾Ƶξ� �����ϰ� ������� ����ϱ� ���� ť
    public Queue<string> rollingTextQue = new Queue<string>();

    //���� �޼���ť�� �޼����� ����ϴ� �Լ�
    public void AddRollingMSG(string _msg)
    {
        rollingTextQue.Enqueue(_msg);
    }

    //�ڷ�ƾ���� �޼����� ���� ��ٷȴٰ� 
    //�޼����� ���� �ش� �޼����� ���
    public System.Collections.IEnumerator RollIngMSG()
    {
        float moveTime = 10;

        //����
        //NetManager.instance.AddRollingMSG("�ȳ��ϼ���");
        //NetManager.instance.AddRollingMSG("���Ű� ȯ���ؿ�");
        //NetManager.instance.AddRollingMSG("12312412312412415123123");
        //NetManager.instance.AddRollingMSG("�����ٶ󸶹ٻ������īŸ����");

        while (true)
        {
            //ť�� �޼����� ����ִ��� üũ
            if (rollingTextQue.Count > 0)
            {
                //ĵ������ Ȱ��ȭ
                RollingCavans.SetActive(true);
                //�ؽ�Ʈ�� ť�� ����ִ� ù��° �޼����� �Է�
                RollingText.text = rollingTextQue.Dequeue();

                //������ ���̰� 10���� ũ��
                if (RollingText.text.Length > 10)
                {
                    //�޼����� ���� ���̿� ���� ������� �ð��� ������
                    //�ð��� ���Ƿ� ���ѰŶ� �ʿ信 ���� �����ؼ� ���� �˴ϴ�
                    moveTime = moveTime + RollingText.text.Length / 2;
                }
                else
                {
                    //10���� ������ �⺻������ ����
                    moveTime = 10;
                }

                //�ؽ�Ʈ�� contents size filter ��� ���ڿ� �°� ui����� �ڵ����� �������ִ� ����� ����� �ؽ�Ʈ�� ������ �̵��� �ؾ��ϴ��� ����ϱ� ���� �����
                //ForceUpdateCanvases �Լ��� contents size filter�� ����� �������� �ʰ� �ݿ��Ǳ⿡ ������ �ݿ������ִ� �Լ�
                Canvas.ForceUpdateCanvases();
                RollingText.transform.DOLocalMoveX(960, 0f);//�߾���(1920/2) �Ѱ����� �ݸ�ŭ ���������� �̵� �� ���� �ʱ���ġ�� ���� 
                RollingText.transform.DOLocalMoveX(-RollingText.rectTransform.rect.width + (-960), moveTime).SetEase(Ease.Linear);//�߾���(1920/2) �Ѱ����� �ݸ�ŭ �������� �̵��� ���� �ؽ�Ʈ�� ���̸�ŭ �� �̵��ϰ�

                //�ؽ�Ʈ�� �̵����� �ð����� �ڷ�ƾ ���
                // Utill.WaitForSeconds �Լ��� WaitForSeconds�� ��Ȱ�� �ϱ� ���� ���� ��ƿ�Լ�
                yield return Utill.WaitForSeconds(moveTime);
            }
            else
            {
                //ť�� �޼����� ������ ĵ���� ��Ȱ��ȭ
                RollingCavans.SetActive(false);
            }

            yield return null;
        }
    }
}
