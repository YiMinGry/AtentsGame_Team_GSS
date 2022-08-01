using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRange : MonoBehaviour
{
    private Unit unit;
    [SerializeField]
    private int targetNum;
    const int TARGETNUM = 10000;
    private float attackCool = 0;
    private Unit targetUnit;
    Animator anim;
    protected ParticleSystem fire;

    public void TargetReset()
    {
        targetNum = TARGETNUM;
    }

    private void Start()
    {
        targetNum = TARGETNUM;
        unit = transform.parent.GetComponent<Unit>();
        attackCool = unit.RangeInterval-0.5f;
        anim = unit.GetComponent<Animator>();
        fire = unit.transform.Find("fire").gameObject.GetComponent<ParticleSystem>();
        fire.Stop();
    }

    private void FixedUpdate()
    {
        //anim.SetInteger("State", (int)UnitState.walk);
        //anim.SetBool("isRangeAttack", false);
    }

    private void OnTriggerStay(Collider other)
    {
        
        Unit unitOther = other.GetComponent<Unit>();
        if(!unit.IsMelee)                               //근접공격중이 아니면
        {
            if ((other.CompareTag("Unit") || other.CompareTag("Enemy"))&& (unit.tag != other.tag)) //적이면
            {
                Debug.Log("원거리 온");
                if (targetNum >= unitOther.UnitNum) //targetNum은 초기:10000 or 선봉의 unitNum 고로 후방은 저 조건에 만족 못 함(움직일 때 리셋하면 안되네)
                {
                    if(targetNum==TARGETNUM)        //타겟이 초기화 됐다면
                    {
                        targetNum = unitOther.UnitNum;
                        attackCool = unit.RangeInterval;
                    }
                    
                    attackCool -= Time.fixedDeltaTime;
                    anim.SetInteger("State", (int)UnitState.range);
                    //anim.SetBool("isRangeAttack", true);
                    unit.IsRange = true;

                    if (attackCool < 0)
                    {
                        unitOther.TakeDamage(unit.Attack);
                        attackCool = unit.RangeInterval;
                        if(this.CompareTag("Enemy"))
                        {
                            
                        }
                        fire.Play();
                    }

                }
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        TargetReset();
    }

}
