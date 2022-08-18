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
        if(!unit.IsMelee)                               //������������ �ƴϸ�
        {
            if ((other.CompareTag("Unit") || other.CompareTag("Enemy"))&& (unit.tag != other.tag)) //���̸�
            {
                
                if (targetNum >= unitOther.UnitNum) //targetNum�� �ʱ�:10000 or ������ unitNum ��� �Ĺ��� �� ���ǿ� ���� �� ��(������ �� �����ϸ� �ȵǳ�)
                {
                    if(targetNum==WRONGTARGETNUM)        //Ÿ���� �ʱ�ȭ �ƴٸ�
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
