using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimConChanger : MonoBehaviour
{
    public delegate void Callback();

    public enum PRState
    {
        move, stop
    }
    public PRState pRState = new PRState();

    public Animator animator;
    public Vector3 moveDir;
    float speed = 0;
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

            rgBody.velocity = transform.TransformDirection(localVelocity);

            animator.SetFloat("xDir", localVelocity.x);
            animator.SetFloat("zDir", localVelocity.z);

            if (localVelocity.x == 0 && localVelocity.z == 0)
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

    public void ForcedStandeing()
    {
        rgBody.velocity = Vector3.zero;
        animator.SetBool("isMove", false);
        walkTime = 0;
        Run(1);
    }
}
