using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player_00run : MonoBehaviour
{
    MiniGame5_Player_Action actions;
    Animator anim;
    Rigidbody rigid;
    CapsuleCollider coli;

    public float jumpPower = 3.0f;
    public bool isJumping = false;
    int jumpCount = 0;

    Vector3 origin_coliPos = new Vector3(0.0f, 0.27f, 0.0f);
    float origin_coliHeight = 0.54f;
    Vector3 onSlide_coliPos = new Vector3(0.0f, 0.22f, -0.15f);
    float onSlide_coliHeight = 0.46f;

    public bool isSuperBig = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        coli = GetComponent<CapsuleCollider>();

        actions = new MiniGame5_Player_Action();
    }

    private void OnEnable()
    {
        actions.Player.Enable();

        actions.Player.Jump.performed += onJump;
        actions.Player.Slide.started += onSlide;
        actions.Player.Slide.canceled += onSlide;
    }

    private void OnDisable()
    {
        actions.Player.Slide.canceled -= onSlide;
        actions.Player.Slide.started -= onSlide;
        actions.Player.Jump.performed -= onJump;

        actions.Player.Disable();
    }

    private void onJump(InputAction.CallbackContext obj)
    {
        Jump();
    }

    private void onSlide(InputAction.CallbackContext obj)
    {
        Slide(obj);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent.CompareTag("Ground"))
        {
            if (isJumping == true)
            {
                anim.SetBool("isJumping", false);
                isJumping = false;
            }
            jumpCount = 0;
        }
    }

    private void Update()
    {
        if (isJumping == true)
        {
            rigid.AddForce(-transform.up * jumpPower * 0.7f);
        }

        SuperTime_Big();
    }

    void Jump()
    {
        if (jumpCount == 0 && isJumping == false)
        {
            anim.SetBool("isJumping", true);
            rigid.velocity = Vector3.zero;
            rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);

            isJumping = true;
            jumpCount = 1;
        } 
        else if (jumpCount == 1)
        {
            anim.SetBool("Roll", true);
            rigid.velocity = Vector3.zero;
            rigid.AddForce(transform.up * jumpPower * 1.2f, ForceMode.Impulse);

            jumpCount = 2;
        }
    }

    void Slide(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetBool("isSlide", true);
            coli.center = onSlide_coliPos;
            coli.height = onSlide_coliHeight;
        }
        else if (context.canceled)
        {
            anim.SetBool("isSlide", false);
            coli.center = origin_coliPos;
            coli.height = origin_coliHeight;
        }
    }

    void SuperTime_Fast()
    {
        //?????? ????
    }

    void SuperTime_Big()
    {
        if (isSuperBig)
            transform.DOScale(9.0f, 0.5f);
        else
            transform.DOScale(4.0f, 0.5f);
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        Debug.Log("Die");
    }
}
