using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AnimConChanger animConChanger;

    int curEventTypr = 0;//플레이어가 상호작용할때 어떤 애니메이션을 실행해야할지 저장
    string nextMoveScenes = "";//씬이동할때 다음씬이 무엇인지 이름저장
    string openUIName = "";//상호작용으로 ui를 열고 닫기 위해 이름 저장


    public void Update()
    {
        //움직이기 위한 함수
        animConChanger.Walk(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        //animConChanger의 상태에 따라서 제한적으로 실행하기 위한 스위치문
        //FSM 야매 버전 같은 느낌으로 생각하시면 될듯합니다.
        switch (animConChanger.pRState)
        {
            case AnimConChanger.PRState.move://움직일때

                //구르기
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animConChanger.Roll();
                }

                break;

            case AnimConChanger.PRState.stop://멈춰있을때


                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //열려있는 팝업이 없을때
                    if (openUIName.Equals(""))
                    {                    
                        //핸드폰 on off
                        animConChanger.OpenPhone();
                    }
                    else
                    {
                        //팝업이 열려있으면 닫기
                        EventManager.Invoke("CloseUI", openUIName);
                    }
                }

                //핸드폰이 안열려있을때//중복실행 방지
                if (animConChanger.isHandCam == false)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        switch (curEventTypr)//1앉기 2줍기
                        {
                            case 1:
                                animConChanger.PlayerFnc(1, () =>
                                {
                                    //미니게임 아케이드 머신에 앉을경우를 상정하여
                                    //해당 씬으로 이동하게
                                    StartCoroutine(GotoNextScene(nextMoveScenes));
                                });

                                break;
                            case 2:
                                animConChanger.PlayerFnc(0, () =>
                                {
                                    //ui팝업 활성화
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
        //플레이어가 닿은 콜라이어의 오브젝트 이름을 받아와서 이름에 맞는 이벤트 실행
        string _name = other.gameObject.name;

        switch (_name)
        {
            //미니게임들
            case "MG_S_01":
            case "MG_S_02":
            case "MG_S_03":
            case "MG_S_04":
            case "MG_S_05":
                curEventTypr = 1;//앉기
                nextMoveScenes = _name;//이동할 씬이름
                other.transform.parent.GetComponent<Outline>().enabled = true;//닿은 오브젝트의 아웃라인을 활성화
                EventManager.Invoke("ActiveFInfo", "미니게임 : " + _name);//툴팁표시
                break;
                //팝업
            case "Ranking":
            case "Friends":
            case "Gacha":
            case "Achivement":
            case "GSS_Desktop":       
                curEventTypr = 2;//모션없이 디폴트로 2 이후 다른 모션 생기면 변경 예정
                openUIName = _name;
                other.transform.parent.GetComponent<Outline>().enabled = true;
                EventManager.Invoke("ActiveFInfo", _name);
                break;
            //위 이름이 아닌 다른오브젝트면 무시
            default:
                curEventTypr = 0;
                nextMoveScenes = "";
                break;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        //플레이어가 빠져나온 오브젝트의 이름으로 정보 초기화
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
