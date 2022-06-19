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
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Vector3 angles = transform.eulerAngles;
                x = angles.y;
                y = angles.x;


                if (target)
                {
                    dist -= 1 * Input.mouseScrollDelta.y;

                    if (dist < 0.5f)
                    {
                        dist = 1;
                    }

                    if (dist >= 9)
                    {
                        dist = 9;
                    }

                    x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
                    y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

                    if (y < 3)
                    {
                        y = 3;
                    }

                    if (y > 85)
                    {
                        y = 85;
                    }

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
