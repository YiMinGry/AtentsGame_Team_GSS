using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;
    public float dist = 4f;

    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;
    private float x = 0.0f;
    private float y = 0.0f;

    public bool isFpsMode = false;

    public Vector2 camXLimit;//x왼쪽으로 최대치//y오른쪽로 최대치//메인씬 : 5,-5//데브씬 :5, -5
    public Vector2 camZLimit;//x안쪽으로 최대치//밖으로 최대치//메인씬 : 5,9.5//데브씬 : 0, 8

    [Header("Camera Angle")] //카메라 거리 조절용
    [Range(1.5f, 6.0f)]
    public float zAngle = 1.5f;

    void Update()
    {
        if (isFpsMode == true) //마우스로 시점 조정
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                //마우스 커서 활성화
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                //마우스 커서 숨기기
                Cursor.lockState = CursorLockMode.Locked;

                Vector3 angles = transform.eulerAngles;
                x = angles.y;
                y = angles.x;


                //따라다닐 대상이 있는경우
                if (target)
                {
                    //마우스 휠로 줌인 줌아웃 하기위해 저장
                    dist -= 1 * Input.mouseScrollDelta.y;

                    //줌인아웃 최소최대 거리 설정
                    if (dist < 0.5f)
                    {
                        dist = 1;
                    }

                    if (dist >= 9)
                    {
                        dist = 9;
                    }

                    //캐릭터의 기본 회전값에 마우스를 움직이는 만큼 메인카메라가 움직이게
                    x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
                    y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

                    //위아래 시점 제한
                    if (y < 3)
                    {
                        y = 3;
                    }

                    if (y > 85)
                    {
                        y = 85;
                    }

                    //위에서 계산한 결과로 카메라에 적용
                    Quaternion rotation;
                    Vector3 position;

                    rotation = Quaternion.Euler(y, x, 0);
                    position = rotation * new Vector3(0.0f, 0.0f, -dist) + target.position + new Vector3(0.0f, 1, 0.0f);

                    transform.rotation = rotation;
                    transform.position = position;

                }
            }
        }
        else //고정 시점
        {
            Cursor.lockState = CursorLockMode.Confined;

            //카메라 앵글
            Vector3 camPos;

            camPos = new Vector3(target.position.x, transform.position.y, target.position.z + zAngle);

            //카메라에 제한영역이 설정되어있으면 더이상 움직이지 않게
            if (!camXLimit.Equals(Vector2.zero))
            {
                if (camXLimit.x < camPos.x)
                {
                    camPos.x = camXLimit.x;
                }
                if (camXLimit.y > camPos.x)
                {
                    camPos.x = camXLimit.y;
                }

                if (camZLimit.x > camPos.z)
                {
                    camPos.z = camZLimit.x;
                }
                if (camZLimit.y < camPos.z)
                {
                    camPos.z = camZLimit.y;
                }
            }

            transform.position = camPos;

            transform.LookAt(target);
        }
    }

}
