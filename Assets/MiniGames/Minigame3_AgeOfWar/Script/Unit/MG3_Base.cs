using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_Base : MG3_Unit
{
    public override int Hp
    {
        get => hp;
        set
        {
            hp = value;
            hpBarImage.fillAmount = (float)hp / (float)hpMax;
            

            if (hp < 1)
            {

                if(transform.CompareTag("Unit"))
                {
                    MG3_GameManager.Inst.score = Mathf.Min((int)MG3_GameManager.Inst.PlayTime, 3600);
                    Debug.Log("ÆÐ¹è");
                }
                else
                {
                    MG3_GameManager.Inst.score = Mathf.Max(3600-(int)MG3_GameManager.Inst.PlayTime, 0)+5000;
                    Debug.Log("½Â¸®");
                }

                MG3_GameManager.Inst.GameOver();
            }

        }
    }
    public int HpMax 
    { 
        get => hpMax;
        set 
        { 

            hp += value-hpMax;
            hpMax = value;
            hpBarImage.fillAmount = (float)hp / (float)hpMax;
        } 
    }
    override protected void Awake()
    {
        hpMax = 150;
        UnitNum= 10000;
        hp = hpMax;
        SetHpBar();
    }
    override protected void Start()
    {
        hp = hpMax;
    }
    override protected void FixedUpdate()
    {
        
    }
    override protected void OnTriggerEnter(Collider other)
    {
        
    }
    protected override void OnTriggerExit(Collider other)
    {
     
    }
    //protected override void OnTriggerStay(Collider other)
    //{
        
    //}
}
