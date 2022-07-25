using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Level4 : Fish_Enemy
{
    private float coolTime = 1.5f;

    private void Start()
    {
        fishLevel = 4;
        direction = new Vector3(1.0f, 0.5f, 0.0f);
    }


    protected override void Move()
    {
        base.Move();
        coolTime -= Time.deltaTime;
        if (coolTime <= 0)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        direction.y = -direction.y;
        coolTime = 1.5f;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        fishScore = 800;
        base.OnCollisionEnter(other);
    }
}
