using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Level6 : Fish_Enemy
{
    protected override void OnCollisionEnter(Collision other)
    {
        fishScore = 2000;
        base.OnCollisionEnter(other);
    }
}
