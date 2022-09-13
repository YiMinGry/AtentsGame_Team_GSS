using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_Obastacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!MiniGame5_GameManager.Inst.Player.IsSuperMode)
            {
                MiniGame5_GameManager.Inst.Player.Damaged();
                MiniGame5_GameManager.Inst.Life -= 0.1f;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
