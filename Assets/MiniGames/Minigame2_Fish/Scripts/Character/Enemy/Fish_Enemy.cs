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

    protected virtual void OnCollisionEnter(Collision other) // Collision 안에 들어왔을 때
    {
        if (other.collider.CompareTag("MG2_Fish_Border")) // Border가 들어왔을 때 Destroy
        {
            Destroy(this.gameObject);
        }
        else if (other.collider.CompareTag("Player")) // 플레이어가 들어왔을 때
        {
            if (MG2_GameManager.Inst.Stage < fishLevel) // 플레이어 레벨(스테이지)보다 물고기 레벨이 더 높거나 같으면 HP 1 감소 후 물고기 Destroy
            {
                Vector3 effectPoint = other.collider.ClosestPoint(transform.position);  // 플레이어와 물고기가 닿은 지점
                MG2_GameManager.Inst.mg2_EffectManager.MakeDamageEffect(effectPoint);   // 위 지점에 데미지 이펙트 생성
                MG2_GameManager.Inst.HealthPoint--;
                AudioManager.Inst.PlaySFX("Water3");
                Destroy(this.gameObject);
            }
            else // 물고기가 더 낮으면 스코어 증가 후 물고기 Destroy
            {
                Vector3 effectPoint = other.collider.ClosestPoint(transform.position);  // 플레이어와 물고기가 닿은 지점
                MG2_GameManager.Inst.mg2_EffectManager.MakeEatEffect(effectPoint);   // 위 지점에 데미지 이펙트 생성
                MG2_GameManager.Inst.Score += fishScore;
                AudioManager.Inst.PlaySFX("Water1");
                Destroy(this.gameObject);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other) // 트리거 안에 들어왔을 때
    {
        if (other.CompareTag("Player")) // 플레이어를 피해 달아남
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
            if (MG2_GameManager.Inst.Stage < fishLevel) // 플레이어보다 레벨이 높을 때
            {
                FishAbility(); 
            }
        }
    }

    protected virtual void FishAbility()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * (-1.0f)); // 플레이어 방향으로 쫓아감  
    }

}



