using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public AnimConChanger animConChanger;
    [SerializeField] Text titleText;
    int curEventTypr = 0;//�÷��̾ ��ȣ�ۿ��Ҷ� � �ִϸ��̼��� �����ؾ����� ����
    string nextMoveScenes = "";//���̵��Ҷ� �������� �������� �̸�����
    string openUIName = "";//��ȣ�ۿ����� ui�� ���� �ݱ� ���� �̸� ����
    Transform sitPos;
    public bool isOpenUI = false;
    public bool isArcadeUIOn = false;

    MinigameImageUI minigameImageUI;

    [SerializeField]
    private Transform[] mfPos;
    [SerializeField]
    private bool[] mfPosCheck = new bool[3];
    public GameObject[] mfs = new GameObject[3];
    private void Awake()
    {
        EventManager.Regist("MF_Refresh", MF_Refresh);

        MFInit();
    }

    public void MFInit()
    {
        //�κ� ���Խ� �̴�ģ�� ����
        int _idx = 0;
        foreach (var _data in MFDataManager.instance.Ret3MF())
        {
            if (_data != null)
            {
                GameObject _mf = Instantiate(_data.prefab);
                _mf.transform.position = mfPos[_idx].transform.position;
                _mf.AddComponent<FollowPet>().target = mfPos[_idx];
                _mf.transform.localScale = mfPos[_idx].localScale;
                _mf.name = _data.id.ToString();
                mfPosCheck[_idx] = true;
                mfs[_idx] = _mf;
                Collider mfCollider = _mf.GetComponent<Collider>(); // Player가 미니친구 콜라이더에 밀리는 문제 방지
                if (mfCollider != null)
                {
                    mfCollider.enabled = false;
                }
                _idx++;
            }
        }
    }


    public void MF_Refresh(string _str = "")
    {
        for (int i = 0; i < mfs.Length; i++)
        {
            mfPosCheck[i] = false;
            Destroy(mfs[i]);
        }

        int _idx = 0;
        foreach (var _data in MFDataManager.instance.Ret3MF())
        {
            if (_data != null)
            {
                GameObject _mf = Instantiate(_data.prefab);
                _mf.transform.position = mfPos[_idx].transform.position;
                _mf.AddComponent<FollowPet>().target = mfPos[_idx];
                _mf.transform.localScale = mfPos[_idx].localScale;
                _mf.name = _data.id.ToString();
                mfPosCheck[_idx] = true;
                mfs[_idx] = _mf;
                Collider mfCollider = _mf.GetComponent<Collider>();
                if (mfCollider != null)
                {
                    mfCollider.enabled = false;
                }

            }
            _idx++;
        }
    }


    public void Update()
    {

        if (isOpenUI)
        {
            animConChanger.Walk(0, 0);
        }
        else
        {
            //�����̱� ���� �Լ�
            animConChanger.Walk(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }


        //animConChanger�� ���¿� ���� ���������� �����ϱ� ���� ����ġ��
        //FSM �߸� ���� ���� �������� �����Ͻø� �ɵ��մϴ�.
        switch (animConChanger.pRState)
        {
            case AnimConChanger.PRState.move://�����϶�

                //������
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animConChanger.Roll();
                }

                break;

            case AnimConChanger.PRState.stop://����������


                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //�����ִ� �˾��� ������
                    if (openUIName.Equals(""))
                    {
                        //�ڵ��� on off
                        animConChanger.TogglePhone();

                        // 폰UI 켜질때 arcade UI는 꺼지도록
                        if (isArcadeUIOn) minigameImageUI.ToggleUI();
                    }
                    else
                    {
                        //�˾��� ���������� �ݱ�
                        EventManager.Invoke("CloseUI", openUIName);
                        isOpenUI = false;
                    }
                }

                //�ڵ����� �ȿ���������//�ߺ����� ����
                if (animConChanger.isHandCam == false)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        switch (curEventTypr)//1�ɱ� 2�ݱ�
                        {
                            case 1:
                                animConChanger.PlayerFnc(1, () =>
                                {
                                    //�̴ϰ��� �����̵� �ӽſ� ������츦 �����Ͽ�
                                    //�ش� ������ �̵��ϰ�
                                    StartCoroutine(GotoNextScene(nextMoveScenes));
                                    StartCoroutine(SitDown(sitPos));
                                });

                                break;
                            case 2:
                                animConChanger.PlayerFnc(0, () =>
                                {
                                    //ui�˾� Ȱ��ȭ
                                    EventManager.Invoke("OpenUI", openUIName);
                                    isOpenUI = true;

                                });
                                break;
                        }
                    }
                }

                break;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        //�÷��̾ ���� �ݶ��̾��� ������Ʈ �̸��� �޾ƿͼ� �̸��� �´� �̺�Ʈ ����
        string _name = other.gameObject.name;
        Transform _transform = other.gameObject.transform;

        switch (_name)
        {
            //�̴ϰ��ӵ�
            case "MG_S_01":
            case "MG_S_02":
            case "MG_S_03":
            case "MG_S_04":
            case "MG_S_05":
                curEventTypr = 1;//�ɱ�
                nextMoveScenes = _name;//�̵��� ���̸�                
                other.transform.parent.GetComponent<Outline>().enabled = true;//���� ������Ʈ�� �ƿ������� Ȱ��ȭ
                EventManager.Invoke("ActiveFInfo", "�̴ϰ��� : " + _name);//����ǥ��
                if(minigameImageUI==null)
                {
                    minigameImageUI=FindObjectOfType<MinigameImageUI>();
                }
                //minigameImageUI.PopUpImage(other.transform.parent.transform, _name);
                minigameImageUI.PopUpImage(other.gameObject.transform, _name);
                isArcadeUIOn = true;
                break;
            //�˾�
            case "SitPos":
                sitPos = _transform;
                break;
            case "Ranking":
            case "Friends":
            case "Gacha":
            case "Achivement":
            case "GSS_Desktop":
                curEventTypr = 2;//��Ǿ��� ����Ʈ�� 2 ���� �ٸ� ��� ����� ���� ����
                openUIName = _name;
                other.transform.parent.GetComponent<Outline>().enabled = true;
                EventManager.Invoke("ActiveFInfo", _name);
                break;
            //�� �̸��� �ƴ� �ٸ�������Ʈ�� ����
            default:
                curEventTypr = 0;
                nextMoveScenes = "";
                break;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        //�÷��̾ �������� ������Ʈ�� �̸����� ���� �ʱ�ȭ
        string _name = other.gameObject.name;

        switch (_name)
        {
            case "MG_S_01":
            case "MG_S_02":
            case "MG_S_03":
            case "MG_S_04":
            case "MG_S_05":
                curEventTypr = 0;
                nextMoveScenes = "";
                //EventManager.Invoke("DeActiveFInfo", _name);
                other.transform.parent.GetComponent<Outline>().enabled = false;
                if(minigameImageUI!=null)
                {
                    minigameImageUI.ToggleUI();
                    isArcadeUIOn = false;
                }
                break;
            case "SitPos":
                sitPos = null;
                break;
            case "Ranking":
            case "Friends":
            case "Gacha":
            case "Achivement":
            case "GSS_Desktop":
                curEventTypr = 0;
                openUIName = "";
                other.transform.parent.GetComponent<Outline>().enabled = false;
                //EventManager.Invoke("DeActiveFInfo", _name);
                other.transform.parent.GetComponent<Outline>().enabled = false;
                if(titleText != null)
                    titleText.enabled = false;
                break;
            default:

                break;
        }

    }

    IEnumerator SitDown(Transform _transform)
    {
        if (sitPos == null)
        {
            sitPos = transform;
        }
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, _transform.position, Time.deltaTime * 2.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_transform.rotation.eulerAngles * (-1.0f)), Time.deltaTime * 2.0f);
            yield return null;
        }
    }
    void OpenArcadeUI_withOutline()
    {

    }

    void ArcadeUI_withOutline()
    {

    }

    IEnumerator GotoNextScene(string _name)
    {
        yield return Utill.WaitForSeconds(1f);
        AudioManager.Inst.StopBGM();
        bl_SceneLoaderManager.LoadScene(_name);
    }
}
