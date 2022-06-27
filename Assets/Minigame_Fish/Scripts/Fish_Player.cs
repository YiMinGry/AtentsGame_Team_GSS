using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Player : MonoBehaviour
{
    [Range(0, 5f)]
    public float Speed = 3;


    Vector3 xyMinLmit;
    Vector3 xyMaxLmit;

    private void Awake()
    {
        xyMinLmit = new Vector3(-7.0f, -4.0f, transform.position.z);
        xyMaxLmit = new Vector3(7.0f, 4.0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(x > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
            //transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, -4.0f), Quaternion.Euler(0f,180.0f,0f));
        }
        if(x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            //transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(0f, 0f, 0f));
        }

        if (transform.position.x < xyMinLmit.x)
        {
            transform.position = new Vector3(xyMinLmit.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xyMaxLmit.x)
        {
            transform.position = new Vector3(xyMaxLmit.x, transform.position.y, transform.position.z);
        }

        if (transform.position.y < xyMinLmit.y)
        {
            transform.position = new Vector3(transform.position.x, xyMinLmit.y, transform.position.z); 
        }
        else if (transform.position.y > xyMaxLmit.y)
        {
            transform.position = new Vector3(transform.position.x, xyMaxLmit.y, transform.position.z);
        }

        Vector2 moveVelocity = new Vector2(x, y) * Speed * Time.deltaTime;

        transform.position += (Vector3)moveVelocity;
    }
}
