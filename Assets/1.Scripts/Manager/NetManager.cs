using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using System.Threading;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DG.Tweening;

public class NetManager : MonoSingleton<NetManager>
{
    //private string IP = "192.168.219.114";
    private string IP = "localhost";

    private string PORT = "5641";
    private string SERVICE_NAME = "/MGServer";

    private WebSocket m_Socket = null;

    public bool isConnect = false;
    [SerializeField]
    private GameObject RollingCavans;
    [SerializeField]
    private Text RollingText;
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

        StartCoroutine(RollIngMSG());
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
        //    Debug.LogWarning("���� ���� ����");
        //}
    }

    private void OnApplicationQuit()
    {
        DisconncectServer();
    }

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
        NetManager.instance.AddRollingMSG("�ȳ��ϼ���");
        NetManager.instance.AddRollingMSG("���Ű� ȯ���ؿ�");
        NetManager.instance.AddRollingMSG("12312412312412415123123");
        NetManager.instance.AddRollingMSG("�����ٶ󸶹ٻ������īŸ����");

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
