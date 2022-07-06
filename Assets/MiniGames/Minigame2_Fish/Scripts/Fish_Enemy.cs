using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Enemy : MonoBehaviour
{
    Rigidbody rigid;
    public float moveSpeed = 10.0f;

    protected int fishScore = 100;
    protected int fishLevel = 1;

    protected Vector3 direction = Vector3.right;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        rigid.MovePosition(transform.position - moveSpeed * direction * this.transform.localScale.z * Time.deltaTime);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("MG2_Fish_Border"))
        {
            Destroy(this.gameObject);
        }
        else if (other.collider.CompareTag("Player"))
        {
            if (MG2_GameManager.Inst.Stage < fishLevel)
            {
                MG2_GameManager.Inst.HealthPoint--;
                Destroy(this.gameObject);
            }
            else
            {
                MG2_GameManager.Inst.Score += fishScore;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            moveSpeed *= 2;
            direction = transform.position - other.transform.position;
            direction.Normalize(); 
            if (transform.position.x > other.transform.position.x) // 물고기가 플레이어의 오른쪽에 있을 때
            {
                if (transform.localScale.z > 0) // 물고기가 왼쪽으로 갈 때
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * (-1.0f)); // 물고기 모양 반대로
                }
            }
            else // 물고기가 플레이어의 왼쪽에 있을 때
            {
                direction = -direction; // 가는 방향 반대로
                if (transform.localScale.z < 0) // 물고기가 오른쪽으로 갈 때
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * (-1.0f)); // 물고기 모양 반대로
                }
            }
            if (MG2_GameManager.Inst.Stage < fishLevel)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * (-1.0f));

            }
        }
    }
}



