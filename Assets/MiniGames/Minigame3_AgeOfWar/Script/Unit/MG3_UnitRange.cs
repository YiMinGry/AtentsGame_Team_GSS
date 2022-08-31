using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_UnitRange : MonoBehaviour
{
    private MG3_Unit unit;
    [SerializeField]
    protected int targetNum;
    protected const int WRONGTARGETNUM = 20000;
    protected float attackCool = 0;
    protected MG3_Unit targetUnit;
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
        revol = MG3_GameManager.Inst.Revolution;
        if(transform.parent.CompareTag("Enemy"))
        {
            revol = MG3_GameManager.Inst.EnemyRevol;
        }
        targetNum = WRONGTARGETNUM;
        unit = transform.parent.GetComponent<MG3_Unit>();
        attackCool = unit.RangeInterval;
        anim = unit.GetComponent<Animator>();
        fire = unit.transform.Find("fire").gameObject.GetComponent<ParticleSystem>();
        fire.Stop();
    }

    

    protected virtual void OnTriggerStay(Collider other)
    {
        
        MG3_Unit unitOther = other.GetComponent<MG3_Unit>();
        if(!unit.IsMelee)                               //������������ �ƴϸ�
        {
            if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.CompareTag(other.tag)))
            {
                
                if (targetNum >= unitOther.UnitNum) //targetNum�� �ʱ�:20000 or ������ unitNum ��� �Ĺ��� �� ���ǿ� ���� �� ��(������ �� �����ϸ� �ȵǳ�)
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
                                MG3_SoundManager.instance.SFXPlay("gun", gunClip);
                            }
                            else
                            {
                                MG3_SoundManager.instance.SFXPlay("arrow", arrowClip);
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
            anim.SetInteger("State", (int)MG3_UnitState.range);
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
                anim.SetInteger("State", (int)MG3_UnitState.walk);
            }
            
        }
        
    }

}
