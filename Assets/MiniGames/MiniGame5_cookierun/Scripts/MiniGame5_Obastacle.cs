using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_Obastacle : MonoBehaviour
{
    public MiniGame5_MapScroll map;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            map.isStop = true;
            MiniGame5_GameManager.Inst.Player.Die();
        }
    }
}
