using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimConChanger : MonoBehaviour
{
    public delegate void Callback();
    public Camera handCam;
    public LookAtPlayer lookAtPlayer;
    public GameObject mainCanvas;

    public bool isFpsMode = false;
    public bool isHandCam = false;

    public enum PRState
    {
        move, stop
    }
    public PRState pRState = new PRState();

    public Animator animator;

    float speed = 1;
    const float minSpeed = 3;
    const float maxSpeed = 5;

    float walkTime = 0;
    public Rigidbody rgBody;

    public bool isMoveingHold = false;

    Vector3 tmpCamPos = new Vector3();

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

        if (func != null)
        {
            func();
        }
    }

    //구르기
    public void Roll()
    {
        if (animator.GetFloat("zDir") > 0.1f && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("landRoll");
        }
    }

    //걷기
    public void Walk(float x, float z)
    {
        if (isMoveingHold == false)
        {
            Vector3 localVelocity = Vector3.ClampMagnitude(new Vector3(x, 0, z), 1) * speed;

            //카메라 시점 조작 모드
            if (isFpsMode == true)
            {
                lookAtPlayer.isFpsMode = true;

                MouseLook();

                rgBody.velocity = transform.TransformDirection(localVelocity);
                animator.SetFloat("xDir", localVelocity.x);
                animator.SetFloat("zDir", localVelocity.z);
            }
            else
            {
                lookAtPlayer.isFpsMode = false;

                rgBody.velocity = localVelocity * -1;

                animator.SetBool("isMove", true);

                animator.SetFloat("zDir", speed);


                if (rgBody.velocity != Vector3.zero)
                {
                    //벨로시티의 방향을 아크탄젠트로 라디안값으로 변환
                    float angleInDegrees = Mathf.Atan2(rgBody.velocity.z, rgBody.velocity.x * -1) * 180 / 3.14f;
                    transform.rotation = Quaternion.Euler(0, angleInDegrees - 90, 0);
                }
            }


            //걷기 달리기 체크
            if (rgBody.velocity.x == 0 && rgBody.velocity.z == 0)
            {
                pRState = PRState.stop;
                animator.SetBool("isMove", false);
                walkTime = 0;
                Run(minSpeed);
            }
            else
            {
                pRState = PRState.move;

                animator.SetBool("isMove", true);
                walkTime += Time.deltaTime;

                if (walkTime > 1f)
                {
                    Run(maxSpeed);
                }
            }

        }
    }

    public void OpenPhone()
    {
        if (animator.GetBool("isPhoneOpen") == false)
        {
            mainCanvas.SetActive(false);
            isHandCam = true;
            handCam.gameObject.SetActive(true);
            isMoveingHold = true;
            animator.SetBool("isPhoneOpen", true);
        }
        else
        {
            mainCanvas.SetActive(true);
            isHandCam = false;
            handCam.gameObject.SetActive(false);
            isMoveingHold = false;
            animator.SetBool("isPhoneOpen", false);
        }

    }

    public void ForcedStandeing()
    {
        rgBody.velocity = Vector3.zero;
        animator.SetBool("isMove", false);
        walkTime = 0;
        Run(1);
    }
}
