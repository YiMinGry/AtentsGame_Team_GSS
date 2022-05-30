using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimConChanger : MonoBehaviour
{
    public Animator animator;
    public Vector3 moveDir;
    float speed = 1;
    public Rigidbody rgBody;

    public bool isMoveingHold = false;

    Vector3 tmpCamPos = new Vector3();

    private void Start()
    {
        StartCoroutine(StateUpDate());
    }

    private void FixedUpdate()
    {

    }

    Vector3 GetMouseScreenToWorldPoint()
    {
        Vector3 mos = Input.mousePosition;
        mos.z = Camera.main.farClipPlane;

        Vector3 dir = Camera.main.ScreenToWorldPoint(mos);

        dir.y = 0;

        return dir;
    }

    public IEnumerator StateUpDate()
    {
        while (true)
        {
            //캐릭터 회전 관련
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
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = 4;
                }
                else
                {
                    speed = 1;

                }
            }

            //상호작용
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (animator.GetBool("isMove") == false)
                    {
                        if (isMoveingHold == true)
                        {
                            animator.SetFloat("lootValue", 0f);
                            //animator.SetFloat("sitValue", 0f);
                            isMoveingHold = false;
                        }
                        else
                        {
                            animator.SetFloat("lootValue", 1f);
                            //animator.SetFloat("sitValue", 1f);
                            isMoveingHold = true;
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (animator.GetBool("isMove") == false)
                    {
                        if (isMoveingHold == true)
                        {
                            //animator.SetFloat("lootValue", 0f);
                            animator.SetFloat("sitValue", 0f);
                            isMoveingHold = false;
                        }
                        else
                        {
                            //animator.SetFloat("lootValue", 1f);
                            animator.SetFloat("sitValue", 1f);
                            isMoveingHold = true;
                        }
                    }
                }
            }

            //구르기
            {
                if (animator.GetFloat("zDir") > 0.1f && Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetTrigger("landRoll");
                }
            }

            //걷기
            {
                if (isMoveingHold == false)
                {
                    float x = Input.GetAxis("Horizontal");
                    float z = Input.GetAxis("Vertical");

                    Vector3 localVelocity = Vector3.ClampMagnitude(new Vector3(x, 0, z), 1) * speed;

                    rgBody.velocity = transform.TransformDirection(localVelocity);

                    animator.SetFloat("xDir", localVelocity.x);
                    animator.SetFloat("zDir", localVelocity.z);

                    if (localVelocity.x == 0 && localVelocity.z == 0)
                    {
                        animator.SetBool("isMove", false);
                    }
                    else
                    {
                        animator.SetBool("isMove", true);
                    }
                }
            }

            yield return null;
        }
    }
}
