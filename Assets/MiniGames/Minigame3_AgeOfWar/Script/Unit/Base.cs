using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Unit
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

                Debug.Log("Á×À½ ¤Ð¤Ð");
                
            }

        }
    }
    override protected void Awake()
    {
        hpMax = 100;
        UnitNum= int.MaxValue;
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
    protected override void OnTriggerStay(Collider other)
    {
        
    }
}
