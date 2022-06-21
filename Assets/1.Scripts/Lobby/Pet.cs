using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    private Animator anim = null;
    private float moveSpeed = 0.2f;

    private bool up = true, down = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("animation",3); // Victory 애니메이션
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;

        if (transform.position.y > 1.5f && up == true)
        {
            up = false;
            down = true;
            moveSpeed = -moveSpeed;
        }

        if (transform.position.y < 1.0f && down == true)
        {
            up = true;
            down = false;
            moveSpeed = -moveSpeed;
        }
    }
}
