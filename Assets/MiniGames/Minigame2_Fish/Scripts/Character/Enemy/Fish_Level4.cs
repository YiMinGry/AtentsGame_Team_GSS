using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Level4 : Fish_Enemy
{
    private void Start()
    {
        fishLevel = 4;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        fishScore = 800;
        base.OnCollisionEnter(other);
    }
}
