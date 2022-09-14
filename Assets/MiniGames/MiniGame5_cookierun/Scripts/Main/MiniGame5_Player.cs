using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using DG.Tweening;

public class MiniGame5_Player : MonoBehaviour
{
    MiniGame5_Player_Action actions;
    Animator anim;
    Rigidbody rigid;
    CapsuleCollider coli;

    bool isFirst = true;
    bool isDie = false;
    bool isReady = false;
    public bool IsReady { 
        get => isReady; 
        private set
        {
            isReady = value;

            if (isFirst)
            {
                MiniGame5_SceneManager.Inst.OnPlayStart();
                isFirst = false;
            }
            else
            {
                MiniGame5_SceneManager.Inst.OnBonusTimeEnd();
            }
        }
    }
    public System.Action OnPlayerReady;

    float startPosY = 10.0f;

    public bool isJumping = false;
    int jumpCount = 0;
    public float jumpPower = 12f;
    public float doubleJumpPower = 30f;
    public int originMag = 5;
    public float magnitude = 5f;

    bool isSlide = false;

    public Vector3 nomalColi_center = new(0f, 0.27f, 0f);
    public Vector2 nomalColi_RadiusHeight = new(0.15f, 0.54f);

    public Vector3 slideColi_center = new(0f, 0.2f, -0.17f);
    public Vector2 slideColi_RadiusHeight = new(0.2f, 0.3f);

    public float jumpColi_height = 0.45f;

    Coroutine coSuper;

    bool isSuperMode = false;
    public bool IsSuperMode
    {
        get => isSuperMode;
        set
        {
            isSuperMode = value;
            Debug.Log($"player super mode = {isSuperMode}");
            if (isSuperMode) coSuper = StartCoroutine(CoSuperTime());
            else StopCoroutine(coSuper);
        }
    } 

    bool isSuperBig = false;
    public bool IsSuperBig
    {
        get => isSuperBig;
        private set
        {
            isSuperBig = value;
            IsSuperMode = value;
            if (!isSuperBig)
                StopCoroutine(coBig);
        }
    }
    bool isSuperBoost = false;
    public bool IsSuperBoost
    {
        get => isSuperBoost;
        private set
        {
            isSuperBoost = value;
            IsSuperMode = value;
        }
    }

    bool isMagnetOn = false;
    public bool IsMagnetOn
    {
        get => isMagnetOn;
        private set
        {
            isMagnetOn = value;
            if (!isMagnetOn)
                StopCoroutine(coMag);
        }
    }

    bool isBonusTime = false;
    public bool IsBonusTime
    {
        get => isBonusTime;
        private set
        {
            isBonusTime = value;
            if (!isBonusTime)
                StopCoroutine(coBonus);
        }
    }

    float magnetRange = 5f;

    Coroutine coMag;
    Coroutine coBig;
    Coroutine coBonus;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        gameObject.AddComponent<CapsuleCollider>();
        gameObject.AddComponent<Rigidbody>();

        coli = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();

        actions = new MiniGame5_Player_Action();

        Init();
    }

    public void Init()
    {
        magnitude = (float)originMag;

        coli.center = nomalColi_center;
        coli.height = 0.3f;
        coli.radius = nomalColi_RadiusHeight.x;

        rigid.constraints =
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        
        if (!this.gameObject.CompareTag("Player"))
        {
            this.gameObject.tag = "Player";
        }

        Utill.ChangeLayersRecursively(this.transform, 6);

        Setting();
    }

    public void Setting()
    {
        isFirst = true;
        isReady = false;
        isJumping = false;
        isSlide = false;
        anim.SetBool("isLanding", false);
        anim.SetBool("isDie", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isSlide", false);
        transform.localPosition = startPosY * Vector3.up;
        transform.localScale = 4.0f * Vector3.one;
    }

    private void OnEnable()
    {
        actions.Player.Enable();

        actions.Player.Jump.performed += onJump;
        actions.Player.Slide.started += onSlide;
        actions.Player.Slide.performed += onSlide;
        actions.Player.Slide.canceled += onSlide;
    }

    private void OnDisable()
    {
        actions.Player.Slide.canceled -= onSlide;
        actions.Player.Slide.performed -= onSlide;
        actions.Player.Slide.started -= onSlide;
        actions.Player.Jump.performed -= onJump;

        actions.Player.Disable();
    }

    private void onJump(InputAction.CallbackContext obj)
    {
        coli.height = jumpColi_height;
        MiniGame5_SoundManager.Inst.PlayJumpCilp();
        Jump();
    }

    private void onSlide(InputAction.CallbackContext obj)
    {
        Slide(obj);
        MiniGame5_SoundManager.Inst.PlaySlideClip();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent.CompareTag("Ground"))
        {
            if (isReady == false)
            {
                anim.SetBool("isLanding", true);
                magnitude = (float)originMag;
                IsReady = true;
            }

            if (isJumping == true)
            {
                anim.SetBool("isJumping", false);
                isJumping = false;
                if (!isSlide && !isDie) coli.height = nomalColi_RadiusHeight.y;
            }
            jumpCount = 0;
        }
    }

    private void Start()
    {
        IsSuperMode = true;

        coli.center = nomalColi_center;
        coli.height = nomalColi_RadiusHeight.y;
        coli.radius = nomalColi_RadiusHeight.x;
    }

    private void Update()
    {
        if (!isDie)
        {
            //if (jumpCount > 0) rigid.AddForce(magnitude * jumpCount * -transform.up);
            //else rigid.AddForce(magnitude * -transform.up);

            if (transform.localPosition.y > 4f)
            {
                rigid.velocity += Vector3.up * Physics.gravity.y * (magnitude - 1) * jumpCount * Time.deltaTime;
            }

            MagNet();
        }
    }

    public IEnumerator CoSuperTime()
    {
        isSuperMode = true;
        yield return new WaitForSeconds(5f);
        isSuperMode = false;
    }

    void Jump()
    {
        if (!isBonusTime)
        {
            if (jumpCount == 0 && isJumping == false)
            {
                anim.SetBool("isJumping", true);
                rigid.velocity = Vector3.zero;
                rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);

                isJumping = true;
                jumpCount = 1;
            }
            else if (jumpCount == 1)
            {
                anim.SetBool("Roll", true);
                rigid.velocity = Vector3.zero;
                rigid.AddForce(transform.up * doubleJumpPower, ForceMode.Impulse);

                jumpCount = 2;
            }
        }
        else
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(transform.up * 5, ForceMode.Impulse);
            Debug.Log("player bonus jump");
        }
    }

    void Slide(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetBool("isSlide", true);
            isSlide = true;
        }
        else if (context.performed)
        {
            coli.center = slideColi_center;
            coli.height = slideColi_RadiusHeight.y;
            coli.radius = slideColi_RadiusHeight.x;
            
            rigid.AddForce(-transform.up * jumpPower, ForceMode.Impulse);
        }
        else if (context.canceled)
        {
            anim.SetBool("isSlide", false);
            coli.center = nomalColi_center;
            coli.height = nomalColi_RadiusHeight.y;
            coli.radius = nomalColi_RadiusHeight.x;

            isSlide = false;
        }
    }

    public void Damaged()
    {
        MiniGame5_SoundManager.Inst.PlayDamageClip();
        anim.SetTrigger("Damage");
    }

    public void Die()
    {
        anim.SetBool("isDie", true);
        isDie = true;

        MiniGame5_SoundManager.Inst.PlayDieClip();
        Debug.Log("Player Die");

        actions.Player.Slide.canceled -= onSlide;
        actions.Player.Slide.started -= onSlide;
        actions.Player.Jump.performed -= onJump;

        actions.Player.Disable();
        //coli.enabled = false;

        Destroy(rigid, 2f);
        Destroy(coli, 2f);
        Destroy(gameObject, 5f);
        Destroy(GetComponent<MiniGame5_Player>());
    }

    void MagNet()
    {
        if (isMagnetOn)
        {
            //가져오고자 하는 오브젝트가 트리거일 경우 QueryTriggerInteraction.Collide 체크
            Collider[] cols = Physics.OverlapSphere((0.7f * Vector3.up) + transform.position, magnetRange, LayerMask.GetMask("Item"), QueryTriggerInteraction.Collide);
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].GetComponent<MiniGame5_Item>().OnMagnet((0.7f * Vector3.up) + transform.position);
            }
        }
    }

    public void MagnetModeOn()
    {
        coMag = StartCoroutine(CoTurnOnMagnet());
    }

    IEnumerator CoTurnOnMagnet()
    {
        isMagnetOn = true;

        yield return new WaitForSeconds(5f);

        IsMagnetOn = false;
    }

    public void BigModeOn()
    {
        coBig = StartCoroutine(CoTurnOnBig());
    }

    IEnumerator CoTurnOnBig()
    {
        isSuperBig = true;
        transform.DOScale(10f, 1f).SetEase(Ease.InOutBounce);
        anim.speed = 0.5F;

        yield return new WaitForSeconds(5f);

        transform.DOScale(4f, 1f).SetEase(Ease.InOutBounce);
        yield return new WaitForSeconds(1f);
        anim.speed = 1F;
        IsSuperBig = false;
    }


    void Boost()
    {

    }

    public void StartBonusTime()
    {
        coBonus = StartCoroutine(CoStartBonusTime());
    }

    IEnumerator CoStartBonusTime()
    {
        anim.SetBool("isMoveToBonusTime", true);

        yield return new WaitForSeconds(0.5f);
        transform.localPosition = new(transform.localPosition.x, 38.7f, transform.localPosition.z);

        anim.SetBool("isBonusTime", true);
        anim.SetBool("isMoveToBonusTime", false);
        isBonusTime = true;

        magnitude = 5;
    }

    public void EndBonusTime()
    {
        coBonus = StartCoroutine(CoEndBonusTime());
    }

    IEnumerator CoEndBonusTime()
    {
        isBonusTime = false;
        anim.SetBool("isBonusTime", false);

        //transform.localPosition = new(transform.localPosition.x, 10f, transform.localPosition.z);
        yield return new WaitForSeconds(0.1f);

        isReady = false;
        IsBonusTime = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isMagnetOn)
        {
            Handles.color = Color.cyan;
            Handles.DrawWireDisc((0.7f * Vector3.up) + transform.position, transform.right, magnetRange);
        }
    }
#endif
}
