using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Level2 : Fish_Enemy
{
    private void Start()
    {
        fishLevel = 2;
    }


    protected override void OnCollisionEnter(Collision other)
    {
        fishScore = 400;
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (fishLevel <= MG2_GameManager.Inst.Stage)
        {
            base.OnTriggerEnter(other);
        }
    }
}
