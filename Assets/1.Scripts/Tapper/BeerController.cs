using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerController : MonoBehaviour
{
    [SerializeField]
    protected Transform beer;
    [SerializeField]
    protected SpriteRenderer sprite;
    [SerializeField]
    Transform arm;
    [SerializeField]
    GameObject fx;
    public virtual void SetFill(float amount)
    {
        if (amount > 1f)
        {
            amount = 1f;
            fx.SetActive(false);
            fx.SetActive(true);
        }
        else 
        {
            fx.SetActive(false);
        }

        sprite.material.SetFloat("_Cutoff", amount);
    }

    public bool GetFill()
    {
        return sprite.material.GetFloat("_Cutoff") >= 1 ? true : false;
    }

    protected virtual void Update()
    {
        transform.position = arm.position;
    }
}
