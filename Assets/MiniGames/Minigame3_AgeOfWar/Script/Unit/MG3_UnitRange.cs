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
    
    private int waitingNum=0;

    private Vector3 rayOffest;

    public void TargetReset()
    {
        targetNum = WRONGTARGETNUM;
    }
    
    protected virtual void Start()
    {

        
        targetNum = WRONGTARGETNUM;
        unit = transform.parent.GetComponent<MG3_Unit>();
        attackCool = unit.RangeInterval;
        anim = unit.GetComponent<Animator>();
        fire = unit.transform.Find("fire").gameObject.GetComponent<ParticleSystem>();
        fire.Stop();

        rayOffest = unit.transform.right * -0.1f + unit.transform.up * 0.5f;
    }

    

    protected virtual void OnTriggerStay(Collider other)
    {
        
        MG3_Unit unitOther = other.GetComponent<MG3_Unit>();
        if(unit.IsRange)                               //������������ �ƴϸ�
        {
            if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.CompareTag(other.tag)))
            {
                
                if (targetNum >= unitOther.UnitNum) //targetNum�� �ʱ�:20000 or ������ unitNum ��� �Ĺ��� �� ���ǿ� ���� �� ��(������ �� �����ϸ� �ȵǳ�)
                {
                    if(targetNum==WRONGTARGETNUM)        //Ÿ���� �ʱ�ȭ �ƴٸ�
                    {
                        targetNum = unitOther.UnitNum;
                        attackCool = Random.Range(-0.1f, 0.1f);
                    }
                    
                    attackCool -= Time.fixedDeltaTime;
                    
                    
                    if (attackCool < 0)
                    {
                        
                        attackCool = unit.RangeInterval;
                        anim.SetTrigger("Attack");
                        
                    }
                }
            }
        }
        
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        //���̸�
        if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && !transform.parent.CompareTag(other.tag)&&!unit.IsMelee)
        {
            waitingNum++;
            anim.SetInteger("State", (int)MG3_UnitState.range);
            unit.IsRange = true;

        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        //���̸�
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
