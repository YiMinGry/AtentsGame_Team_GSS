using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Level1 : Fish_Enemy
{
    private void Start()
    {
        fishLevel = 1;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        fishScore = 200;
        base.OnCollisionEnter(other);
    }


}
