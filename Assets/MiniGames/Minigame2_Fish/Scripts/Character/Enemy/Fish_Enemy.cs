using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Enemy : MonoBehaviour
{
    protected Rigidbody rigid;

    [SerializeField]
    float moveSpeed = 10.0f;

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

    protected virtual void OnCollisionEnter(Collision other) // Collision �ȿ� ������ ��
    {
        if (other.collider.CompareTag("MG2_Fish_Border")) // Border�� ������ �� Destroy
        {
            Destroy(this.gameObject);
        }
        else if (other.collider.CompareTag("Player")) // �÷��̾ ������ ��
        {
            if (MG2_GameManager.Inst.Stage < fishLevel) // �÷��̾� ����(��������)���� ����� ������ �� ���ų� ������ HP 1 ���� �� ����� Destroy
            {
                Vector3 effectPoint = other.collider.ClosestPoint(transform.position);  // �÷��̾�� ����Ⱑ ���� ����
                MG2_GameManager.Inst.mg2_EffectManager.MakeDamageEffect(effectPoint);   // �� ������ ������ ����Ʈ ����
                MG2_GameManager.Inst.HealthPoint--;
                AudioManager.Inst.PlaySFX("Water3");
                Destroy(this.gameObject);
            }
            else // ����Ⱑ �� ������ ���ھ� ���� �� ����� Destroy
            {
                Vector3 effectPoint = other.collider.ClosestPoint(transform.position);  // �÷��̾�� ����Ⱑ ���� ����
                MG2_GameManager.Inst.mg2_EffectManager.MakeEatEffect(effectPoint);   // �� ������ ������ ����Ʈ ����
                MG2_GameManager.Inst.Score += fishScore;
                AudioManager.Inst.PlaySFX("Water1");
                Destroy(this.gameObject);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other) // Ʈ���� �ȿ� ������ ��
    {
        if (other.CompareTag("Player")) // �÷��̾ ���� �޾Ƴ�
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
            if (MG2_GameManager.Inst.Stage < fishLevel) // �÷��̾�� ������ ���� ��
            {
                FishAbility(); 
            }
        }
    }

    protected virtual void FishAbility()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * (-1.0f)); // �÷��̾� �������� �Ѿư�  
    }

}



