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
            if (transform.position.x > other.transform.position.x) // ����Ⱑ �÷��̾��� �����ʿ� ���� ��
            {
                if (transform.localScale.z > 0) // ����Ⱑ �������� �� ��
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * (-1.0f)); // ����� ��� �ݴ��
                }
            }
            else // ����Ⱑ �÷��̾��� ���ʿ� ���� ��
            {
                direction = -direction; // ���� ���� �ݴ��
                if (transform.localScale.z < 0) // ����Ⱑ ���������� �� ��
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * (-1.0f)); // ����� ��� �ݴ��
                }
            }
            if (MG2_GameManager.Inst.Stage < fishLevel)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * (-1.0f));

            }
        }
    }
}



