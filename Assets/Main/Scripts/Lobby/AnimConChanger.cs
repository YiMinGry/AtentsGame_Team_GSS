using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimConChanger : MonoBehaviour
{
    public delegate void Callback();//각종 상호작용 이벤트를 실행시키기 위한 델리게이트

    public LookAtPlayer lookAtPlayer;//메인 카메라가 캐릭터를 바라보게하기위한 클래스
    public GameObject mainCanvas;//메인 캔버스

    [Space(10)]
    public Camera PhoneMode_PhoneFocus;//핸드폰 모션용 카메라

    public Transform PhoneMode_PlayerFocus;

    [Space(10)]
    public bool isHandCamMode = false;

    [Space(10)]
    public bool isFpsMode = false;//고정시점모드, 1인칭 캐릭터 뒷모습 모드 설정하는 변수
    public bool isHandCam = false;//핸드폰 모션 연출중인지 체크용 변수
    public System.Action<bool> PhoneAnim;

    [Space(10)]
    public Animator animator;
    public Rigidbody rgBody;

    //캐릭터가 무슨 상태인지 체크하기위한 이넘타입
    public enum PRState
    {
        move, stop
    }
    //위 이넘타입을 사용하기위해 선언
    public PRState pRState = new PRState();

    float speed = 1;//캐릭터 걷기 속도
    const float minSpeed = 3;//캐릭터 걷기 기본속도 
    const float maxSpeed = 5;//캐릭터 달리기 속도

    float walkTime = 0;//걷기를 일정시간 지속하면 자동으로 달리기상태로 변환시키기 위한 시간저장용 변수

    public bool isMoveingHold = false;//캐릭터가 움직이지 못하게 하기 위한 변수

    Coroutine coPhoneUI;

    //마우스의 위치를 추적하여 방향으로 변환하여 리턴하는 함수
    Vector3 GetMouseScreenToWorldPoint()
    {
        Vector3 mos = Input.mousePosition;
        mos.z = Camera.main.farClipPlane;

        Vector3 dir = Camera.main.ScreenToWorldPoint(mos);

        dir.y = 0;

        return dir;
    }

    //캐릭터 회전 관련
    public void MouseLook()
    {
        if (animator.GetBool("isMove") == true)
        {
            if (isMoveingHold == false)
            {
                transform.LookAt(GetMouseScreenToWorldPoint());
            }
        }
    }

    //달리기
    public void Run(float _speed)
    {
        speed = _speed;
    }

    //상호작용
    //상호작용키를 눌렀을때 실행되는 함수
    //_type 으로 실행시킬 애니메이션 클립을 설정
    //func으로 해당 상호작용에서 실행할 기능을 받아옴
    //콜백(델리게이트)를 사용한 이유는 같은 앉기 기능이라도 실행해야할 기능이 달라질수있으므로
    //실행시켜야할 기능을 받아와서 실행
    public void PlayerFnc(int _type, Callback func)
    {
        switch (_type)
        {
            case 0:

                break;

            case 1:

                if (animator.GetBool("isSit") == false)
                {
                    isMoveingHold = true;
                    animator.SetBool("isSit", true);
                }
                else
                {
                    isMoveingHold = false;
                    animator.SetBool("isSit", false);
                }

                break;

            case 2://줍기

                if (animator.GetFloat("lootValue") > 0f)
                {
                    isMoveingHold = true;
                    animator.SetFloat("lootValue", 0f);
                }
                else
                {
                    isMoveingHold = false;
                    animator.SetFloat("lootValue", 1f);

                }
                break;
            default:
                return;
        }

        //콜백이 없을경우에는 무시하도록
        if (func != null)
        {
            func();
        }
    }

    //구르기
    public void Roll()
    {
        //앞으로 움직이는 상태일경우에만 구르기를 실행할수있도록
        if (animator.GetFloat("zDir") > 0.1f && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("landRoll");
        }
    }

    //걷기
    public void Walk(float x, float z)
    {
        //플레이어가 움직일수 있는 상태일때
        if (isMoveingHold == false)
        {
            //ClampMagnitude는 벡터의 최대 길이를 정하는 함수로
            //0~1사이의 값만 리턴되게 설정하였습니다
            Vector3 localVelocity = Vector3.ClampMagnitude(new Vector3(x, 0, z), 1) * speed;

            //카메라 시점 조작 모드
            if (isFpsMode == true)
            {
                lookAtPlayer.isFpsMode = true;

                MouseLook();

                //TransformDirection 로컬의 방향벡터를 월드방향벡터로 변경하기 위해 사용
                rgBody.velocity = transform.TransformDirection(localVelocity);
                animator.SetFloat("xDir", localVelocity.x);
                animator.SetFloat("zDir", localVelocity.z);
            }
            else
            {
                lookAtPlayer.isFpsMode = false;

                //캐릭터 모델링의 기본 방향이 카메라를 처다보고있는 방향이므로
                //앞으로 가기위헤서는 방향은 반전시켜줘야합니다
                //그러기위해 벨로시티의 값을 * -1로 반전시켜줍니다
                rgBody.velocity = localVelocity * -1;

                animator.SetBool("isMove", true);

                animator.SetFloat("zDir", speed);


                if (rgBody.velocity != Vector3.zero)
                {
                    //벨로시티의 방향을 아크탄젠트로 라디안값으로 변환
                    //키보드의 입력값인 rgBody.velocity은 방향이 아닌 움직이는 힘의 크기므로
                    //회전을 하기위해서는 움직임과 동시에 velocity에있는 각 축의 힘의 크기를 이용하여
                    //회전을 시켜야합니다.
                    //게임패드, 조이스틱, 오락실 같은 스틱으로 방향을 정해 움직이는걸 생각하시면 쉽습니다.
                    //입력된 xy값을 방향에 맵핑하여 캐릭터의 로테이션을 돌리는 기능입니다.
                    float angleInDegrees = Mathf.Atan2(rgBody.velocity.z, rgBody.velocity.x * -1) * 180 / 3.14f;
                    transform.rotation = Quaternion.Euler(0, angleInDegrees - 90, 0);
                }
            }


            //걷기 달리기 체크
            if (rgBody.velocity.x == 0 && rgBody.velocity.z == 0)
            {//움직이지 않을때
                pRState = PRState.stop;//상태를 정지상태로 만들어줍니다.
                animator.SetBool("isMove", false);
                walkTime = 0;
                Run(minSpeed);
            }
            else
            {
                pRState = PRState.move;//상태를 움직임상태로 만들어줍니다.

                animator.SetBool("isMove", true);
                walkTime += Time.deltaTime;//움직이기 시작해서 시간을 누적하고

                if (walkTime > 1f)//누적한 시간이 1초보다 커지면 달리기 상태로 만들어줍니다.
                {
                    Run(maxSpeed);
                }
            }

        }
    }

    public void TogglePhone()
    {
        // 폰 닫혀있으면 열고
        if (animator.GetBool("isPhoneOpen") == false)
        {
            AudioManager.Inst.PlaySFX("EffectSound_Pop");
            if (isHandCamMode) // 1인칭
            {
                
                isHandCam = true;
                PhoneMode_PhoneFocus.gameObject.SetActive(true);
            }
            else // 3인칭
            {
                PhoneAnim?.Invoke(true);
                //PlayerFocusCam.gameObject.SetActive(true);
                coPhoneUI = StartCoroutine(lookAtPlayer.DoMove_PhoneModeOn(this.transform, PhoneMode_PlayerFocus));
            }
            isMoveingHold = true;
            animator.SetBool("isPhoneOpen", true);
        }
        else //열려있으면 닫음
        {
            AudioManager.Inst.PlaySFX("EffectSound_Pop2");
            if (isHandCamMode)
            {
                isHandCam = false;
                PhoneMode_PhoneFocus.gameObject.SetActive(false);
            }
            else
            {
                PhoneAnim?.Invoke(false);
                //PlayerFocusCam.gameObject.SetActive(false);
                coPhoneUI = StartCoroutine(lookAtPlayer.DoMove_PhoneModeOff());
            }
            isMoveingHold = false;
            animator.SetBool("isPhoneOpen", false);
        }
        StopCoroutine(coPhoneUI);
    }

    

    public void ForcedStandeing()
    {
        rgBody.velocity = Vector3.zero;
        animator.SetBool("isMove", false);
        walkTime = 0;
        Run(1);
    }
}
