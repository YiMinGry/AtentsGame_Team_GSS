using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum oderState
{
    move, getBeer, stop, back
}

public class OderMove : MonoBehaviour
{
    public oderState state;

    [Range(0, 1f)]
    public float Scale = 1f;
    public float Speed = 3;
    public Animator animator;
    CharacterActionHandle characterActionHandle;

    Vector3 xMinLmit;
    Vector3 xMaxLmit;

    float x;
    int prDir = 1;

    const float maxWaitingTime = 20f;
    const float maxlastWaitingTime = 5f;
    float waitingTime = 0;
    float lastWaitingTime = 0;

    private void Awake()
    {
        characterActionHandle = animator.GetComponent<CharacterActionHandle>();
        xMinLmit = new Vector3(-2f, transform.position.y, transform.position.z);
        xMaxLmit = new Vector3(0f, transform.position.y, transform.position.z);
    }
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(OderMoveStart());
    }

    IEnumerator OderMoveStart()
    {
        while (true)
        {
            switch (state)
            {
                case oderState.move:
                    x = Random.Range(0, 2);
                    break;
                case oderState.getBeer:
                    break;
                case oderState.stop:
                    x = 0;
                    break;
                case oderState.back:
                    x = -1;
                    break;
            }
            yield return Utill.WaitForSeconds(1f);
        }
    }
    private void Update()
    {
        Move();
    }
    void Move()
    {
        waitingTime += Time.deltaTime;


        if (transform.position.x < xMinLmit.x)
        {
            transform.position = xMinLmit;
        }
        else if (transform.position.x > xMaxLmit.x)
        {
            transform.position = xMaxLmit;
            state = oderState.stop;
        }

        if (waitingTime > maxWaitingTime || lastWaitingTime > maxlastWaitingTime)
        {
            state = oderState.back;
        }

        if (x != 0)
        {
            prDir = (int)x;
        }

        Vector2 moveVelocity = new Vector2(x, 0) * (Speed * Scale) * Time.deltaTime;


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
    }
}
