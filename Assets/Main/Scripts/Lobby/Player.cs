using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AnimConChanger animConChanger;

    int curEventTypr = 0;//�÷��̾ ��ȣ�ۿ��Ҷ� � �ִϸ��̼��� �����ؾ����� ����
    string nextMoveScenes = "";//���̵��Ҷ� �������� �������� �̸�����
    string openUIName = "";//��ȣ�ۿ����� ui�� ���� �ݱ� ���� �̸� ����


    public void Update()
    {
        //�����̱� ���� �Լ�
        animConChanger.Walk(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


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
                        animConChanger.OpenPhone();
                    }
                    else
                    {
                        //�˾��� ���������� �ݱ�
                        EventManager.Invoke("CloseUI", openUIName);
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
                                });

                                break;
                            case 2:
                                animConChanger.PlayerFnc(0, () =>
                                {
                                    //ui�˾� Ȱ��ȭ
                                    EventManager.Invoke("OpenUI", openUIName);
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
                break;
                //�˾�
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
                other.transform.parent.GetComponent<Outline>().enabled = false;
                EventManager.Invoke("DeActiveFInfo", _name);
                break;

            case "Ranking":
            case "Friends":
            case "Gacha":
            case "Achivement":
            case "GSS_Desktop":
                curEventTypr = 0;
                openUIName = "";
                other.transform.parent.GetComponent<Outline>().enabled = false;
                EventManager.Invoke("DeActiveFInfo", _name);
                break;
            default:

                break;
        }

    }



    IEnumerator GotoNextScene(string _name)
    {
        yield return Utill.WaitForSeconds(1f);

        bl_SceneLoaderManager.LoadScene(_name);
    }
}
