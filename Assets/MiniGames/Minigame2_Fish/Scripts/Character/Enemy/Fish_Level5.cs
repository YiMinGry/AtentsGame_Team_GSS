using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Level5 : Fish_Enemy
{
    private void Start()
    {
        fishLevel = 5;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        fishScore = 1000;
        base.OnCollisionEnter(other);
    }
}
