using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerController : MonoBehaviour
{
    [SerializeField]
    Transform beer;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    Transform arm;
    [SerializeField]
    GameObject fx;
    public void SetFill(float amount)
    {
        if (amount > 1f)
        {
            amount = 1f;
            fx.SetActive(false);
            fx.SetActive(true);
        }

        sprite.material.SetFloat("_Cutoff", amount);
    }

    public bool GetFill()
    {
        return sprite.material.GetFloat("_Cutoff") >= 1 ? true : false;
    }

    private void Update()
    {
        transform.position = arm.position;
    }
}
