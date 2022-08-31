using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_FallingCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MiniGame5_GameManager.Inst.Life = 0f;
    }
}
