
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Unit : MonoBehaviour
{
    
    [SerializeField] protected float moveSpeed = 1.0f;
    [SerializeField] protected int unitNum = 0;
    [SerializeField] protected int hp;
    protected float attackCool = 0.0f;
    protected int waitingNum = 0;
    protected bool isMelee = false;
    protected bool isRange = false;
    private float nowMoveSpeed;
    //���ֺ� ����----------------------------------------------------------
    [SerializeField] protected int attack = 5;
    [SerializeField] protected int hpMax = 20;
    [SerializeField] protected int cost = 15;
    [SerializeField] protected int exp = 15;
    [SerializeField] protected float meleeTime = 1.3f;
    [SerializeField] protected float rangeTime = 1.0f;
    [SerializeField] protected float buildTime  = 3.0f;


    //�ҷ��;��ϴ� ������Ʈ---------------------------------------------------
    private UnitRange unitRange;
    Rigidbody rigid;
    private Animator anim;
    protected Unit targetUnit;
    protected BoxCollider[] unitBox = new BoxCollider[2];
    protected ParticleSystem blood;
    //hpbar-----------------------------------------------------------------
    public GameObject hpBarPrefabs;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    protected Canvas uiCanvas;
    protected Image hpBarImage;


  
   



    //������Ƽ-----------------------------------------------------------------------------------------------------------------
    public bool IsMelee { get => isMelee; }
    public bool IsRange { set => isRange = value; }
    public int Cost { get => cost; }
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            hpBarImage.fillAmount = (float)hp / (float)hpMax;
            blood.Play();

            if(hp<1)
            {
                StartCoroutine(Die());
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                if(this.CompareTag("Enemy"))
                {
                    GameManager.Inst.Gold += cost; 
                    GameManager.Inst.Exp += cost;
                }
            }
                
        }
    }
    
    public int Attack { get => attack; }
    public float RangeInterval { get => rangeTime; }
    public int UnitNum//�����ʿ��� unitnum�� unitCount�� �־���
    {
        get => unitNum;
        set => unitNum = value;
    }

    // �Լ���------------------------------------------------------------------
    public void SetUnitStat(int _attack, int _hpMax, int _cost, int _exp,float _buildTime, float _melee, float _range = 1.0f)
    {
        attack = _attack;
        hpMax = _hpMax;
        cost = _cost;
        exp = _exp;
        buildTime = _buildTime;
        meleeTime = _melee;
        rangeTime = _range;
    }
    public void SetUnitStat(UnitData unitData)
    {
        attack=unitData.attack;
        hpMax= unitData.hpMax;  
        cost=unitData.cost;
        exp= unitData.exp;
        buildTime= unitData.buildTime;
        meleeTime= unitData.meleeTime;
        rangeTime= unitData.rangeTime;  
    }
    IEnumerator Die()
    {
        anim.SetTrigger("Dead");
        for(int i=0;i<2;i++)
        {
            unitBox[i].center = new Vector3(0, 10, 0);
        }
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
    
    
    public void TakeDamage(int _attack)
    {
        Hp -= _attack;
        
    }
    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefabs, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];//0���� �ڱ��ڽ��̶���
        var _hpbar = hpBar.GetComponent<UnitHp>();
        _hpbar.targetTr = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }
   

    
    //�̺�Ʈ �Լ� ---------------------------------------------------------------------------------

    private void Awake()
    {
        hp = hpMax;
        SetHpBar();
        nowMoveSpeed = moveSpeed;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        unitRange = GetComponentInChildren<UnitRange>();
        attackCool = meleeTime;
        unitBox[0] = GetComponent<BoxCollider>();
        unitBox[1] = GetComponentInChildren<UnitRange>().gameObject.GetComponent<BoxCollider>();
        
        
    }

    private void Start()
    {
        blood =transform.Find("blood").gameObject.GetComponent<ParticleSystem>();
        blood.Stop();
        anim.SetInteger("State", (int)UnitState.walk);
    }
    private void FixedUpdate()
    {
        rigid.MovePosition(transform.position + nowMoveSpeed * transform.forward * Time.fixedDeltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Unit unitOther = other.GetComponent<Unit>();
        if (other.CompareTag(this.gameObject.tag))//���� �±׸�
        {
            if (unitNum > unitOther.UnitNum)            // ������ ������ enter�Ǹ�
            {
                nowMoveSpeed = 0;
                if (!isRange)                           //���Ÿ� ������� �ƴϸ�
                {
                    anim.SetInteger("State", (int)UnitState.Idle);
                    //anim.Setbool("isWaiting", true)
                }
                waitingNum++;
            }
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Unit")) //���� enter
        {
            nowMoveSpeed = 0;
            anim.SetInteger("State", (int)UnitState.melee);
            //anim.SetBool("isFighting", true);
            isMelee = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Unit unitOther = other.GetComponent<Unit>();
        if ((other.CompareTag(this.gameObject.tag))&& (unitNum > unitOther.UnitNum))//���� �±��̰� ������ ������ exit�ϸ�
        {
            if(waitingNum<=1)
            {
                nowMoveSpeed = moveSpeed;
                anim.SetInteger("State", (int)UnitState.walk);
                
                //anim.SetBool("isWaiting", false);
                waitingNum--;
            }
            else
            {
                waitingNum--;
            }
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Unit")) //���� exit �ϸ�(������)
        {
            nowMoveSpeed = moveSpeed;
            anim.SetInteger("State", (int)UnitState.walk);
            //anim.SetBool("isFighting", false);
            isMelee = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Unit unitOther = other.GetComponent<Unit>();

        if (!(CompareTag(other.tag)) && (other.CompareTag("Unit") || other.CompareTag("Enemy")))    //���̸�
        {
            if (unitOther != targetUnit)                                   //���� �д����� �ƴϸ�
            {
                targetUnit = unitOther;
                attackCool = meleeTime+Random.Range(-0.1f,0.1f);
            }
            attackCool -= Time.fixedDeltaTime;
            if (attackCool < 0)
            {
                unitOther.TakeDamage(Attack);
                attackCool = meleeTime;
            }
        }
    }
}

