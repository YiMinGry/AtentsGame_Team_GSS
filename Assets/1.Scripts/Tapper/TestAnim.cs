using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.Play("Debuff_Stun");
    }
}
