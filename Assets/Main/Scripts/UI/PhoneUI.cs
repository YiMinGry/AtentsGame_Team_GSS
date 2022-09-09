using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PhoneUI : MonoBehaviour
{
    //Data================
    public GameObject page2_miniThumbUI;

    //Phone================
    Button[] tabBtns;
    Transform[] pages;

    //Page1================
    Text page1_PlayerName;
    Text page1_HaveMini;
    Text page1_HaveCoin1;
    Text page1_HaveCoin2;
    InputField page1_Comment;

    //Page2================
    Transform page2_MiniDetail_NotData;
    Transform page2_MiniDetail_Data;
    Image page2_MiniDetail_Image;
    Text page2_MiniDetail_Name;
    Text page2_MiniDetail_Buff;
    GameObject page2_MiniDetail_Check;
    GameObject page2_MiniDetail_NotCheck;
    Transform page2_MiniList;
    [SerializeField]
    GameObject _mfItem;
    [SerializeField]
    List<MFItemInfo> _mfs;

    //Page3================
    Text page3_AchiveCount;
    Text page3_TotalScore;
    Text page3_TotalRank;
    Transform page3_sidePage;
    //Transform page3_AchiveList;
    Transform page3_RankList;
    Text[] page3_RankText;
    

    private void Awake()
    {
        Transform tab = transform.GetChild(0).Find("Tabs");
        tabBtns = new Button[tab.childCount];
        for (int i = 0; i < tab.childCount; i++)
        {
            tabBtns[i] = tab.GetChild(i).GetComponent<Button>();
        }

        Transform page = transform.GetChild(0).Find("Pages");
        pages = new Transform[page.childCount];
        for (int i = 0; i < page.childCount; i++)
        {
            pages[i] = page.GetChild(i);
        }

        tabBtns[0].onClick.AddListener(() => OpenPage(0));
        tabBtns[1].onClick.AddListener(() => OpenPage(1));
        tabBtns[2].onClick.AddListener(() => OpenPage(2));
        tabBtns[3].onClick.AddListener(() => OpenPage(3));

        //Page1=================================================
        page1_PlayerName = pages[0].Find("Contents").Find("PlayerName").GetComponent<Text>();
        page1_HaveMini = pages[0].Find("Contents").Find("HaveMini").GetComponent<Text>();
        page1_HaveCoin1 = pages[0].Find("Contents").Find("HaveCoins").Find("Coin1").GetComponent<Text>();
        page1_HaveCoin2 = pages[0].Find("Contents").Find("HaveCoins").Find("Coin2").GetComponent<Text>();
        page1_Comment = pages[0].Find("TextBlock").Find("InputField").GetComponent<InputField>();

        //Page2=================================================
        page2_MiniDetail_NotData = pages[1].Find("MiniDetail").Find("NoData");
        page2_MiniDetail_Data = pages[1].Find("MiniDetail").Find("Data");
        page2_MiniDetail_Image = page2_MiniDetail_Data.Find("Image").GetComponent<Image>();
        page2_MiniDetail_Name = page2_MiniDetail_Data.Find("Name").GetComponent<Text>();
        page2_MiniDetail_Buff = page2_MiniDetail_Data.Find("Buff").GetComponent<Text>();
        page2_MiniDetail_Check = page2_MiniDetail_Data.Find("Check").gameObject;
        page2_MiniDetail_NotCheck = page2_MiniDetail_Data.Find("NotCheck").gameObject;
        page2_MiniList = pages[1].Find("MiniList");
        

        _mfs = new List<MFItemInfo>();

        for (int i = 0; i < MFDataManager.instance.mfarr.Length; i++)
        {
            GameObject _mf = Instantiate(_mfItem, page2_MiniList);
            _mfs.Add(_mf.GetComponent<MFItemInfo>());
            _mfs[i].Init(ref MFDataManager.instance.mfarr[i], page2_MiniDetail_Check, page2_MiniDetail_NotCheck);

            int idx = i;

            _mfs[idx].SetCallBack(
                (string str) =>
                {
                    page2_MiniDetail_NotData.gameObject.SetActive(MFDataManager.instance.mfarr[idx].isHave == true ? false : true);//미소유
                    page2_MiniDetail_Data.gameObject.SetActive(MFDataManager.instance.mfarr[idx].isHave == true ? true : false);//소유

                    page2_MiniDetail_Image.sprite = MFDataManager.instance.mfarr[idx].sprite;
                    page2_MiniDetail_Name.text = MFDataManager.instance.mfarr[idx].friendName;
                    page2_MiniDetail_Buff.text = MFDataManager.instance.mfarr[idx].buff_MiniGame5;
                    page2_MiniDetail_Check.SetActive(MFDataManager.instance.mfarr[idx].isChoose == true ? true : false);
                    page2_MiniDetail_NotCheck.SetActive(MFDataManager.instance.mfarr[idx].isChoose == true ? false : true);

                    page2_MiniDetail_Check.GetComponent<Button>().onClick.RemoveAllListeners();
                    page2_MiniDetail_Check.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        MFDataManager.instance.Send_HaveMF(MFDataManager.instance.mfarr[idx].id);

                        SetPage2_Refresh();

                    });


                    page2_MiniDetail_NotCheck.GetComponent<Button>().onClick.RemoveAllListeners();
                    page2_MiniDetail_NotCheck.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        MFDataManager.instance.Send_ChoiceMF(MFDataManager.instance.mfarr[idx].id);

                        SetPage2_Refresh();
                    });

                }
                );
        }


        //Page3=================================================
        page3_AchiveCount = pages[2].Find("Achievements").GetComponent<Text>();
        page3_TotalScore = pages[2].Find("Ranking").Find("TotalScore").GetComponent<Text>();
        page3_TotalRank = pages[2].Find("Ranking").Find("TotalRank").GetComponent<Text>();
        page3_sidePage = pages[2].Find("SideContents");
        page3_RankList = page3_sidePage.Find("RankingDetail").Find("ScrollView").Find("Viewport");
        page3_RankText = new Text[10];
        for(int i=0; i<10; i++)
        {
            page3_RankText[i] = page3_RankList.GetChild(i).GetComponent<Text>();
        }


    }
    private void OnEnable()
    {
        NetEventManager.Regist("ReadMyAllRanking", S2CL_ReadMyAllRanking);//서버에서 ReadRanking 커멘드로 패킷이 올경우 실행
        NetEventManager.Regist("TotalRanking", S2CL_TotalRanking);
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        SetPage1();
        SetPage2();
        SetPage3();
    }

    void OpenPage(int index)
    {
        AudioManager.Inst.PlaySFX("EffectSound_Pop2");
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }

        pages[index].gameObject.SetActive(true);
        //Debug.Log($"open {index} page");
    }

    public void SetPage1()
    {
        if (UserDataManager.instance.nickName != "") page1_PlayerName.text = UserDataManager.instance.nickName;
        else if (UserDataManager.instance.nickName == "") page1_PlayerName.text = "Noname";
        //page1_HaveMini = $"{UserDataManager.instance.보유한미니친구갯수}마리";
        page1_HaveCoin1.text = UserDataManager.instance.coin1.ToString();
        page1_HaveCoin2.text = UserDataManager.instance.coin2.ToString();
        //if (UserDataManager.instance.comment != "") page1_Comment = UserDataManager.instance.comment;
    }

    public void SetPage2()
    {
        // scriptable object 메인에서 관리 어케 할건지 정해서 거기서 정보 받아서 아래 처리
        SetPage2_detail();
        // page2_MiniList 요소 채우기 Instantiate
    }

    public void SetPage2_detail(MiniFriendData data = null)
    {
        if (data == null)
        {
            page2_MiniDetail_Data.gameObject.SetActive(false);
            page2_MiniDetail_NotData.gameObject.SetActive(true);
        }
        else
        {
            page2_MiniDetail_NotData.gameObject.SetActive(false);
            page2_MiniDetail_Data.gameObject.SetActive(true);

            // page2_MiniDetail 에 들어갈 요소 채우기
        }
    }

    public void SetPage2_Refresh()
    {
        for (int i = 0; i < _mfs.Count; i++)
        {
            _mfs[i].Refresh();
        }
    }


    public void SetPage3()
    {
        CL2S_TotalRanking();
        UpdateRank();

        UserDataManager.instance.RefreshUserInfo();
        // 미니게임들 완성되고 넣어야할듯한...

        //page3_AchieveCount = 
        //page3_TotalRank = 

        //page3_TotalScore =
        //그 외 디테일 볼 수 있게 하단 버튼에 연결
    }
    //page3함수========================================================================
    void UpdateRank()
    {
        CL2S_ReadMyAllRanking();
        
    }
    
    
    public void CL2S_ReadMyAllRanking()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "ReadMyAllRanking");
        _userData.Add("ID", UserDataManager.instance.ID);

        NetManager.instance.CL2S_SEND(_userData);
    }

    public void S2CL_ReadMyAllRanking(JObject _jdata)
    {
        Debug.Log(_jdata.ToString());
        
        string BaseSceneName = "MG_";
        string SceneName;
        for (int i = 2; i < 6; i++)
        {
            SceneName = $"{BaseSceneName}{i}";
            JArray _data = JArray.Parse(_jdata[SceneName].ToString());

            Debug.Log(_data);
            JObject data = JObject.Parse(_data[10].ToString());

            string rank = $"{data["ranking"]}";
            if (rank == "-1")
            {
                page3_RankText[i * 2 - 2].text = "기록없음";
                page3_RankText[i * 2-1].text = $"--";
            }
            else
            {
                page3_RankText[i * 2 - 2].text = $"{data["ranking"]}위";
                page3_RankText[i * 2-1].text = $"{data["Score"]}점";
            }
        }
    }
    public void S2CL_TotalRanking(JObject _jdata)
    {
        Debug.Log(_jdata);

        JObject _data = JObject.Parse(_jdata["MyRank"].ToString());
        page3_TotalRank.text = $"{_data["MG_Total_Rank"]}";
        page3_RankText[0].text = $"{_data["MG_Total_Rank"]}위";
        page3_TotalScore.text = $"{_data["MG_Total_Score"]}";
        page3_RankText[1].text = $"{_data["MG_Total_Score"]}점";
    }
    public void CL2S_TotalRanking()
    {
        JObject _userData = new JObject();
        _userData.Add("cmd", "TotalRanking");
        _userData.Add("ID", UserDataManager.instance.ID);

        NetManager.instance.CL2S_SEND(_userData);
    }
}
