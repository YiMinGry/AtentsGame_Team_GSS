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

    void Update()
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
                position = rotation * new Vector3(0, 0.0f, -dist) + target.position + new Vector3(0.0f, 1, 0.0f);

                transform.rotation = rotation;
                transform.position = position;

            }
        }
    }

}
