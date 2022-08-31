
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MG3_Unit : MonoBehaviour
{
    
    [SerializeField] protected float moveSpeed = 1.0f;
    [SerializeField] protected int unitNum = 0;
    [SerializeField] protected int hp;
    protected float attackCool = 0.0f;
    protected float realattackCool = 1.0f;
    public int waitingNum = 0;
    protected bool isMelee = false;
    protected bool isRange = false;
    private float nowMoveSpeed;
    protected bool attackStart = true;
    public AudioClip swingClip;
    //유닛별 차등----------------------------------------------------------
    [SerializeField] protected int attack = 5;
    [SerializeField] protected int hpMax = 20;
    [SerializeField] protected int cost = 15;
    [SerializeField] protected int exp = 15;
    [SerializeField] protected float meleeTime = 1.5f;
    [SerializeField] protected float meleedelay = 0.3f;
    [SerializeField] protected float rangeTime = 1.0f;
    [SerializeField] protected float rangedelay = 0.3f;
    [SerializeField] protected float buildTime  = 3.0f;


    //불러와야하는 컴포넌트---------------------------------------------------
    private MG3_UnitRange unitRange;
    Rigidbody rigid;
    private Animator anim;
    protected MG3_Unit targetUnit;
    protected BoxCollider[] unitBox=new BoxCollider[3];
    protected ParticleSystem blood;
    //hpbar-----------------------------------------------------------------
    public GameObject hpBarPrefabs;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    protected Canvas uiCanvas;
    protected Image hpBarImage;


  
   



    //프로퍼티-----------------------------------------------------------------------------------------------------------------
    public bool IsMelee { get => isMelee; }
    public bool IsRange { set => isRange = value; }
    public float RangeDelay { get => rangedelay; }
    public int Cost { get => cost; }
    public virtual int Hp
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
                    MG3_GameManager.Inst.Gold += cost; 
                    MG3_GameManager.Inst.Exp += exp;
                }
            }
                
        }
    }
    
    public int Attack { get => attack; }
    public float RangeInterval { get => rangeTime; }
    public int UnitNum//스포너에서 unitnum에 unitCount를 넣어줌
    {
        get => unitNum;
        set => unitNum = value;
    }

    // 함수부------------------------------------------------------------------
   
    public void SetUnitStat(MG3_UnitData unitData)
    {
        attack=unitData.attack;
        hpMax= unitData.hpMax;  
        hp= unitData.hpMax;
        cost =unitData.cost;
        exp= unitData.exp;
        buildTime= unitData.buildTime;
        meleeTime= unitData.meleeTime;
        rangeTime= unitData.rangeTime;  
    }
    IEnumerator Die()
    {
        anim.SetTrigger("Dead");
        for(int i=0;i<unitBox.Length;i++)
        {
            unitBox[i].center = new Vector3(0, 100, 0);
        }
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
    
    
    public void TakeDamage(int _attack)
    {
        Hp -= _attack;
        
    }
    protected void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefabs, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];//0번은 자기자신이라함
        var _hpbar = hpBar.GetComponent<MG3_UnitHp>();
        _hpbar.targetTr = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }
   

    
    //이벤트 함수 ---------------------------------------------------------------------------------

    virtual protected void Awake()
    {
        hp = hpMax;
        SetHpBar();
        nowMoveSpeed = moveSpeed;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        unitRange = GetComponentInChildren<MG3_UnitRange>();
        attackCool = meleeTime;
        realattackCool = meleedelay;
        //unitBox[0] = GetComponent<BoxCollider>();
        //if(unitRange!=null)
        //{
        //    unitBox[1] = GetComponentInChildren<UnitRange>().gameObject.GetComponent<BoxCollider>();
        //}
        unitBox=GetComponentsInChildren<BoxCollider>();
        
        
        
    }

    virtual protected void Start()
    {
        blood =transform.Find("blood").gameObject.GetComponent<ParticleSystem>();
        blood.Stop();
        anim.SetInteger("State", (int)MG3_UnitState.walk);
    }
    virtual protected void FixedUpdate()
    {
        rigid.MovePosition(transform.position + nowMoveSpeed * transform.forward * Time.fixedDeltaTime);
        if(MG3_GameManager.Inst.IsGameover)
        {
            Destroy(this.gameObject); 
        }
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        MG3_Unit unitOther = other.GetComponent<MG3_Unit>();
        if (other.CompareTag(this.gameObject.tag))//같은 태그면
        {
            if (unitNum > unitOther.UnitNum)            // 나보다 선봉이 enter되면
            {
                nowMoveSpeed = 0;
                if (!isRange)                           //원거리 사격중이 아니면
                {
                    anim.SetInteger("State", (int)MG3_UnitState.Idle);
                    //anim.Setbool("isWaiting", true)
                }
                waitingNum++;
            }
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Unit")) //적이 enter
        {
            nowMoveSpeed = 0;
            anim.SetInteger("State", (int)MG3_UnitState.melee);
            //anim.SetBool("isFighting", true);
            isMelee = true;
        }
    }
    virtual protected void OnTriggerExit(Collider other)
    {
        MG3_Unit unitOther = other.GetComponent<MG3_Unit>();
        if ((other.CompareTag(this.gameObject.tag))&& (unitNum > unitOther.UnitNum))//같은 태그이고 나보다 선봉이 exit하면
        {
            if(waitingNum<=1)
            {
                nowMoveSpeed = moveSpeed;
                anim.SetInteger("State", (int)MG3_UnitState.walk);
                
                //anim.SetBool("isWaiting", false);
                waitingNum--;
            }
            else
            {
                waitingNum--;
            }
        }
        else if ((other.CompareTag("Enemy") || other.CompareTag("Unit"))&& waitingNum <= 1) //적이 exit 하면(죽으면)
        {
            nowMoveSpeed = moveSpeed;
            anim.SetInteger("State", (int)MG3_UnitState.walk);
            //anim.SetBool("isFighting", false);
            isMelee = false;
        }
    }

    virtual protected void OnTriggerStay(Collider other)
    {
        MG3_Unit unitOther = other.GetComponent<MG3_Unit>();

        if (!(CompareTag(other.tag)) && (other.CompareTag("Unit") || other.CompareTag("Enemy")))    //적이면
        {
            if (unitOther != targetUnit)                                   //원래 패던놈이 아니면
            {
                targetUnit = unitOther;
                attackCool = meleeTime+Random.Range(-0.1f,0.1f);
                attackStart = true;
                realattackCool = meleedelay;
            }
            attackCool -= Time.fixedDeltaTime;
            if (attackCool < 0)
            {
               // unitOther.TakeDamage(Attack);
                attackCool = meleeTime;
                attackStart = true;
                realattackCool = meleedelay;
            }
            if(attackStart)
            {
                realattackCool -= Time.fixedDeltaTime;
                if(realattackCool < 0)
                {
                    attackStart = false;
                    MG3_SoundManager.instance.SFXPlay("swing", swingClip);
                    unitOther.TakeDamage(Attack);
                }
            }
        }
    }
}

