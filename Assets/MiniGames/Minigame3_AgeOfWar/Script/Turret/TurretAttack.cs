using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TurretAttack : UnitRange
{
    float attackInterval = 5.0f;
    public float lookSpeed = 1.0f;
    public GameObject projectile;
    Rigidbody rigid;
    public float shootingPower = 10.0f;
    Transform projectileTr;
    public float attackDelay = 0.3f;
    public float attackDelayTimer = 0.3f;
    bool attackStart=false;
    

    protected override void Start()
    {
        targetNum = WRONGTARGETNUM;
        anim = GetComponent<Animator>();
        
        
        projectileTr = transform.GetChild(0).Find("ProjectileTr");
        attackDelayTimer = attackDelay;

    }
    protected override void OnTriggerStay(Collider other)
    {
        Unit unitOther = other.GetComponent<Unit>();
        
        if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.CompareTag(other.tag))) //적이면
        {
            //적이 사정거리내로 왔다
            if (targetNum >= unitOther.UnitNum) //targetNum은 초기:10000 or 선봉의 unitNum 고로 후방은 저 조건에 만족 못 함(움직일 때 리셋하면 안되네)
            {   //첫번째놈이다
                Transform target=unitOther.transform;
                
                if (targetNum == WRONGTARGETNUM)        //타겟이 초기화 됐다면
                {
                    targetNum = unitOther.UnitNum;
                    attackDelayTimer = attackDelay;
                    attackStart = false;
                    //InitializeAttack();
                }

                attackCool -= Time.fixedDeltaTime;

                if (attackCool < 0)
                {
                    //공격 애니메이션 실행
                    //Debug.Log("폭탄받아라!!!!!!!!!!!");
                    attackCool = attackInterval;
                    anim.SetTrigger("Shoot");
                    attackStart = true;
                }
                if(attackStart)
                {
                    attackDelayTimer -= Time.fixedDeltaTime;
                }
                if(attackDelayTimer < 0)
                {
                    attackDelayTimer = attackDelay;
                    TurretShot(target);
                    Debug.Log("빵!!!!!!");
                    attackStart=false;
                }
                LookAtSlow(target);
                double a = Math.Round((double)attackCool, 2);
                double b = Math.Round((double)attackDelayTimer, 2);
                Debug.Log($"cool={a},delay={b},{attackStart}"  );
            }
        }
        
    }
    protected override void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.CompareTag(other.tag)))
        {

            attackStart = false;
            attackDelayTimer = attackDelay;
        }

            base.OnTriggerExit(other);
        
        //anim.SetTrigger("ShootStop");
        //InitializeAttack();
    }

    void LookAtSlow(Transform targetTr)
    {
        Vector3 dir = targetTr.position - transform.position;
        this.transform.rotation=Quaternion.Lerp(this.transform.rotation,Quaternion.LookRotation(dir),Time.deltaTime*lookSpeed);
    }
    public void TurretShot(Transform target)
    {
        GameObject obj = Instantiate(projectile, projectileTr);
        rigid = obj.GetComponent<Rigidbody>();
        Vector3 dir = target.position - transform.position;
        rigid.gameObject.transform.parent = null;
        rigid.isKinematic = false;
        
        rigid.AddForce(dir * shootingPower, ForceMode.Impulse);
    }

    void InitializeAttack()
    {
        attackStart = true;
        attackDelayTimer = attackDelay;
        attackCool = attackInterval;
    }
}
