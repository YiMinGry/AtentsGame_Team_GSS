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

    //    if ((other.CompareTag("Unit") || other.CompareTag("Enemy")) && (!transform.parent.parent.CompareTag(other.tag))) //���̸�
    //    {
    //        //���� �����Ÿ����� �Դ�
    //        if (targetNum >= unitOther.UnitNum) //targetNum�� �ʱ�:10000 or ������ unitNum ��� �Ĺ��� �� ���ǿ� ���� �� ��(������ �� �����ϸ� �ȵǳ�)
    //        {   //ù��°���̴�
    //            targetTr=unitOther.transform;

    //            if (targetNum == WRONGTARGETNUM)        //Ÿ���� �ʱ�ȭ �ƴٸ�
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
    public void DomySetActive()// addEvent ��
    {

        projectileDomy.SetActive(true);
    }
    public void ShotEvent()//addEvent ��
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
