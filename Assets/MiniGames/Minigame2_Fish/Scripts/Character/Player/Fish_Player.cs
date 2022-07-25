using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Player : MonoBehaviour
{
    [Range(0, 5f)]
    public float Speed = 3;

    GameObject[] playerPrefab;

    Vector3 xyMinLmit;
    Vector3 xyMaxLmit;

    private void Awake()
    {
        playerPrefab = new GameObject[5];
        for (int i = 0; i < 5; i++)
            playerPrefab = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            playerPrefab[i] = transform.GetChild(i).gameObject;
        }
        xyMinLmit = new Vector3(-8.0f, -4.0f, transform.position.z);
        xyMaxLmit = new Vector3(8.0f, 4.0f, transform.position.z);
    }

    private void Start()
    {
        MG2_GameManager.Inst.playerLevelChange = PlayerLevelUp;
    }

    float dashTime = 0.1f;          // 대쉬 지속시간
    float dashEffectTime = 0.3f;    // 대쉬 이펙트 지속시간
    float dashCoolTime = 1.0f;      // 대쉬 스킬 쿨타임
    float dashCoolTimeCheck = 1.0f; // 대쉬 스킬 쿨타임 체크
    bool isDash = false;

    // Update is called once per frame
    void Update()
    {
        Move();
        if(dashCoolTimeCheck > 0.0f)
        {
            dashCoolTimeCheck -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && dashCoolTimeCheck <= 0.0f) // Jump 버튼(스페이스바)를 눌렀을 때 대쉬
        {
            isDash = true;
            dashTime = 0.1f;
            dashEffectTime = 0.3f;
            dashCoolTimeCheck = dashCoolTime;
            MG2_GameManager.Inst.mg2_EffectManager.SetDashEffect(true);
            MG2_GameManager.Inst.mg2_EffectManager.MakeDashEffect();
        }
        Dash();
    }

    void Dash()
    {
        if (isDash)
        {
            dashTime -= Time.deltaTime;
            if (dashTime > 0)
            {
                transform.position += Vector3.Lerp(Vector3.zero, -5.0f * transform.right * transform.localScale.x, 5.0f * Time.deltaTime);
            }
            else
            {
                isDash = false;
            }
        }

        if(dashEffectTime > 0)
        {
            dashEffectTime -= Time.deltaTime;
        }
        else
        {
            MG2_GameManager.Inst.mg2_EffectManager.SetDashEffect(false);
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(x > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
            MG2_GameManager.Inst.mg2_EffectManager.SetDashEffectScale(new Vector3(-1,1,1));
        }
        if(x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            MG2_GameManager.Inst.mg2_EffectManager.SetDashEffectScale(new Vector3(1, 1, 1));
        }

        if (transform.position.x < xyMinLmit.x)
        {
            transform.position = new Vector3(xyMinLmit.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xyMaxLmit.x)
        {
            transform.position = new Vector3(xyMaxLmit.x, transform.position.y, transform.position.z);
        }

        if (transform.position.y < xyMinLmit.y)
        {
            transform.position = new Vector3(transform.position.x, xyMinLmit.y, transform.position.z); 
        }
        else if (transform.position.y > xyMaxLmit.y)
        {
            transform.position = new Vector3(transform.position.x, xyMaxLmit.y, transform.position.z);
        }

        Vector2 moveVelocity = new Vector2(x, y) * Speed * Time.deltaTime;

        transform.position += (Vector3)moveVelocity;
    }

    void PlayerLevelUp()
    {
        playerPrefab[MG2_GameManager.Inst.Stage - 1].SetActive(false);
        playerPrefab[MG2_GameManager.Inst.Stage].SetActive(true);
        MG2_GameManager.Inst.mg2_EffectManager.SetLevelUpEffect(true);
    }

}
