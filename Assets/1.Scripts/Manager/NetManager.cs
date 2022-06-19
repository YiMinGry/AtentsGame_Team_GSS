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
        //    Debug.LogWarning("서버 접속 실패");
        //}
    }

    private void OnApplicationQuit()
    {
        DisconncectServer();
    }

    //롤링 공지를 받아두어 저장하고 순서대로 출력하기 위한 큐
    public Queue<string> rollingTextQue = new Queue<string>();

    //공지 메세지큐에 메세지를 등록하는 함수
    public void AddRollingMSG(string _msg)
    {
        rollingTextQue.Enqueue(_msg);
    }

    //코루틴으로 메세지가 오길 기다렸다가 
    //메세지가 오면 해당 메세지를 출력
    public System.Collections.IEnumerator RollIngMSG()
    {
        float moveTime = 10;

        //사용법
        NetManager.instance.AddRollingMSG("안녕하세요");
        NetManager.instance.AddRollingMSG("오신걸 환영해요");
        NetManager.instance.AddRollingMSG("12312412312412415123123");
        NetManager.instance.AddRollingMSG("가나다라마바사아자차카타파하");

        while (true)
        {
            //큐에 메세지가 담겨있는지 체크
            if (rollingTextQue.Count > 0)
            {
                //캔버스를 활성화
                RollingCavans.SetActive(true);
                //텍스트에 큐에 담겨있는 첫번째 메세지를 입력
                RollingText.text = rollingTextQue.Dequeue();

                //문자의 길이가 10보다 크면
                if (RollingText.text.Length > 10)
                {                
                    //메세지의 문자 길이에 따라서 출력해줄 시간을 정해줌
                    //시간은 임의로 정한거라 필요에 따라서 수정해서 쓰면 됩니다
                    moveTime = moveTime + RollingText.text.Length / 2;
                }
                else
                {
                    //10보다 작으면 기본값으로 정함
                    moveTime = 10;
                }

                //텍스트에 contents size filter 라는 글자에 맞게 ui사이즈를 자동으로 조절해주는 기능을 사용함 텍스트가 어디까지 이동을 해야하는지 계산하기 위해 사용함
                //ForceUpdateCanvases 함수는 contents size filter이 기능이 몇프레임 늦게 반영되기에 강제로 반영시켜주는 함수
                Canvas.ForceUpdateCanvases();
                RollingText.transform.DOLocalMoveX(960, 0f);//중앙점(1920/2) 한값에서 반만큼 오른쪽으로 이동 한 곳을 초기위치로 설정 
                RollingText.transform.DOLocalMoveX(-RollingText.rectTransform.rect.width + (-960), moveTime).SetEase(Ease.Linear);//중앙점(1920/2) 한값에서 반만큼 왼쪽으로 이동한 값에 텍스트의 길이만큼 더 이동하게

                //텍스트가 이동중인 시간동안 코루틴 대기
                // Utill.WaitForSeconds 함수는 WaitForSeconds를 재활용 하기 위해 만든 유틸함수
                yield return Utill.WaitForSeconds(moveTime);
            }
            else 
            {
                //큐에 메세지가 없으면 캔버스 비활성화
                RollingCavans.SetActive(false);
            }

            yield return null;
        }
    }
}
