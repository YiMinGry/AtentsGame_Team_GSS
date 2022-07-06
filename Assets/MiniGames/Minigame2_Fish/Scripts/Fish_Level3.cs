using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Level3 : Fish_Enemy
{
    protected override void OnCollisionEnter(Collision other)
    {
        fishScore = 600;
        base.OnCollisionEnter(other);
    }
}
