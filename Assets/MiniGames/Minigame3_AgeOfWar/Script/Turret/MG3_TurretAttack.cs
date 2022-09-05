using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MG3_TurretAttack : MG3_UnitRange
{
    float attackInterval = 3.0f;
    float attackCool;
    public float lookSpeed = 1.0f;
    public GameObject projectile;
    Rigidbody rigid;
    public float shootingPower = 50.0f;
    Transform projectileTr;
    Transform targetTr;
    Transform turretTr;
    float offset;
    [SerializeField] LayerMask UnitLayerMask;
    
    public AudioClip turretClip;
    GameObject projectileDomy;

    private void Awake()
    {
        attackCool = attackInterval;
        projectileDomy = transform.GetChild(0).GetChild(2).gameObject;
        turretTr = transform.parent;
        int turretSlotNum =int.Parse( turretTr.parent.name[turretTr.parent.name.Length - 1].ToString());
        offset = (0.8f + turretSlotNum * 1.2f);

    }
    protected override void Start()
    {
        //targetNum = WRONGTARGETNUM;
        anim = GetComponent<Animator>();


        projectileTr = transform.GetChild(0).Find("ProjectileTr");
        SetTarget();
    }
    //protected override void OnTriggerStay(Collider other)
    //{
    //    MG3_Unit unitOther = other.GetComponent<MG3_Unit>();

    //    if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.parent.CompareTag(other.tag))) //적이면
    //    {
    //        //적이 사정거리내로 왔다
    //        if (targetNum >= unitOther.UnitNum) //targetNum은 초기:10000 or 선봉의 unitNum 고로 후방은 저 조건에 만족 못 함(움직일 때 리셋하면 안되네)
    //        {   //첫번째놈이다
    //            targetTr=unitOther.transform;

    //            if (targetNum == WRONGTARGETNUM)        //타겟이 초기화 됐다면
    //            {
    //                targetNum = unitOther.UnitNum;
    //                attackCool = attackInterval;
    //            }

    //            attackCool -= Time.fixedDeltaTime;

    //            if (attackCool < 0)
    //            {

    //                attackCool = attackInterval;
    //                anim.SetTrigger("Shoot");
    //            }

    //            LookAtSlow(targetTr);

    //        }
    //    }

    //}

    protected override void FixedUpdate()
    {
        if(targetTr!=null)
        {
            attackCool -= Time.fixedDeltaTime;
            //LookAtSlow(targetTr);
            if(attackCool < 0)
            {
                anim.SetTrigger("Shoot");
                attackCool = attackInterval;
            }
        }


    }
    protected override void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.CompareTag(other.tag)))
        {
            if (targetTr == null)
            {
                attackCool = attackInterval;
                SetTarget();
            }
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.CompareTag(other.tag)))
        {
            targetTr = null;
            if (targetTr == null)
            {
                SetTarget();
            }
        }

    }
    public void SetTarget()
    {
        if (Physics.Raycast(turretTr.position - turretTr.up * offset - turretTr.forward, transform.forward, out RaycastHit hitInfo, 12f, UnitLayerMask))
        {
            targetTr = hitInfo.transform;
        }
    }
    public void DomySetActive()// addEvent 함
    {

        projectileDomy.SetActive(true);
    }
    public void ShotEvent()//addEvent 함
    {

        projectileDomy.SetActive(false);
        MG3_SoundManager.instance.SFXPlay("turret", turretClip);
        
        if(targetTr != null)
        {

            TurretShot(targetTr);
        }
        
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
        Vector3 dir = (target.position - transform.position).normalized;
        dir.z = 0;
        rigid.gameObject.transform.parent = null;
        rigid.isKinematic = false;
        
        rigid.AddForce(dir * shootingPower, ForceMode.Impulse);
    }

    
}
