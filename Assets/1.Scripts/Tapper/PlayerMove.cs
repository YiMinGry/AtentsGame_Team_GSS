using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Range(0, 1f)]
    public float Scale = 1f;
    public float Speed = 3;
    public Animator animator;
    CharacterActionHandle characterActionHandle;

    int prDir = 1;
    int lineIdx = 0;
    private void Awake()
    {
        characterActionHandle = animator.GetComponent<CharacterActionHandle>();
    }
    // Update is called once per frame
    void Update()
    {
        if (characterActionHandle._isAttack == false)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (characterActionHandle._isAttack == true)
            {
                return;
            }

            animator.SetTrigger("Attack");
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = 0;//Input.GetAxisRaw("Vertical");

        if (transform.position.x < -1.7)
        {
            transform.position = new Vector3(-1.7f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 1.7)
        {
            transform.position = new Vector3(1.7f, transform.position.y, transform.position.z);
        }

        Vector2 moveVelocity = new Vector2(x, y) * (Speed * Scale) * Time.deltaTime;

        if (x != 0)
        {
            prDir = (int)x;
        }

        if (moveVelocity != Vector2.zero)
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

        transform.position += (Vector3)moveVelocity;
        transform.localScale = new Vector3(prDir * -1, 1, 1) * Scale;


        if (transform.position.x > 0.3f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (lineIdx == 3)
                {
                    return;
                }

                lineIdx++;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (lineIdx == 0)
                {
                    return;
                }

                lineIdx--;
            }
        }

        transform.position = new Vector3(transform.position.x, -0.87f + (0.5f * lineIdx), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        switch (tag)
        {
            case "Rain":
                Time.timeScale = 0;
                break;
        }

    }
}
