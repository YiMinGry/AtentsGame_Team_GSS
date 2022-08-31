using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MG3_TurretAttack : MG3_UnitRange
{
    float attackInterval = 5.0f;
    public float lookSpeed = 1.0f;
    public GameObject projectile;
    Rigidbody rigid;
    public float shootingPower = 10.0f;
    Transform projectileTr;
    public float attackDelay = 0.3f;
    public float attackDelayTimer = 0.3f;
    
    public AudioClip turretClip;
    GameObject projectileDomy;

    private void Awake()
    {
        projectileDomy = transform.GetChild(0).GetChild(2).gameObject;
    }
    protected override void Start()
    {
        targetNum = WRONGTARGETNUM;
        anim = GetComponent<Animator>();
        
        
        projectileTr = transform.GetChild(0).Find("ProjectileTr");
        attackDelayTimer = attackDelay;
       
    }
    protected override void OnTriggerStay(Collider other)
    {
        MG3_Unit unitOther = other.GetComponent<MG3_Unit>();
        
        if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.parent.CompareTag(other.tag))) //적이면
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
                    MG3_SoundManager.instance.SFXPlay("turret", turretClip);

                    attackCool = attackInterval;
                    anim.SetTrigger("Shoot");
                    attackStart = true;
                    projectileDomy.SetActive(false);
                }
                if(attackCool<1.0f)
                {
                    projectileDomy.SetActive(true);
                }
                if(attackStart)
                {
                    attackDelayTimer -= Time.fixedDeltaTime;
                    
                }
                if(attackDelayTimer < 0)
                {
                    
                    attackDelayTimer = attackDelay;
                    TurretShot(target);
                    //Debug.Log("빵!!!!!!");
                    attackStart=false;
                }
                LookAtSlow(target);
               
            }
        }
        
    }
    protected override void OnTriggerEnter(Collider other)
    {
        
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
