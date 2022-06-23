using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerMove : BeerController
{
    public float Speed = 3;

    public override void SetFill(float amount)
    {
        if (amount > 1f)
        {
            amount = 1f;
        }

        sprite.material.SetFloat("_Cutoff", amount);
    }
    protected override void Update()
    {
        Vector2 moveVelocity = new Vector2(-1, 0) * Speed * Time.deltaTime;
        transform.position += (Vector3)moveVelocity;

    }
}
