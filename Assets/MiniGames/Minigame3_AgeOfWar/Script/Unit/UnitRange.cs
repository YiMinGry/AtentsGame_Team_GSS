using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRange : MonoBehaviour
{
    private Unit unit;
    [SerializeField]
    protected int targetNum;
    protected const int WRONGTARGETNUM = 10000;
    protected float attackCool = 0;
    protected Unit targetUnit;
    protected Animator anim;
    private ParticleSystem fire;
    public AudioClip arrowClip;
    public AudioClip gunClip;
    private int revol;
    private int waitingNum=0;
    protected bool attackStart = false;
    private float realattackCool=0;

    public void TargetReset()
    {
        targetNum = WRONGTARGETNUM;
    }
    
    protected virtual void Start()
    {
        revol = GameManager.Inst.Revolution;
        if(transform.parent.CompareTag("Enemy"))
        {
            revol = GameManager.Inst.EnemyRevol;
        }
        targetNum = WRONGTARGETNUM;
        unit = transform.parent.GetComponent<Unit>();
        attackCool = unit.RangeInterval;
        anim = unit.GetComponent<Animator>();
        fire = unit.transform.Find("fire").gameObject.GetComponent<ParticleSystem>();
        fire.Stop();
    }

    

    protected virtual void OnTriggerStay(Collider other)
    {
        
        Unit unitOther = other.GetComponent<Unit>();
        if(!unit.IsMelee)                               //근접공격중이 아니면
        {
            if ((other.CompareTag("Unit") || other.CompareTag("Enemy"))&& (unit.tag != other.tag)) //적이면
            {
                
                if (targetNum >= unitOther.UnitNum) //targetNum은 초기:10000 or 선봉의 unitNum 고로 후방은 저 조건에 만족 못 함(움직일 때 리셋하면 안되네)
                {
                    if(targetNum==WRONGTARGETNUM)        //타겟이 초기화 됐다면
                    {
                        targetNum = unitOther.UnitNum;
                        attackCool = unit.RangeInterval;
                        attackStart = true;
                        realattackCool = unit.RangeDelay;
                    }
                    
                    attackCool -= Time.fixedDeltaTime;
                    
                    
                    if (attackCool < 0)
                    {
                        
                        attackCool = unit.RangeInterval;
                        attackStart = true;
                        realattackCool=unit.RangeDelay;
                        
                    }
                    if(attackStart)
                    {
                        realattackCool -= Time.fixedDeltaTime;
                        if(realattackCool < 0)
                        {
                            attackStart = false;
                            if (revol > 1)
                            {
                                SoundManager.instance.SFXPlay("gun", gunClip);
                            }
                            else
                            {
                                SoundManager.instance.SFXPlay("arrow", arrowClip);
                            }
                            unitOther.TakeDamage(unit.Attack);
                        }
                    }

                }
            }
        }
        
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.CompareTag(other.tag)))
        {
            waitingNum++;
            anim.SetInteger("State", (int)UnitState.range);
            unit.IsRange = true;

        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.CompareTag(other.tag)))
        {
            TargetReset();
            waitingNum--;
            if(waitingNum == 0)
            {
                anim.SetInteger("State", (int)UnitState.walk);
            }
            
        }
        
    }

}
