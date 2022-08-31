using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_Item : MonoBehaviour
{
    public int itemScore = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MiniGame5_GameManager.Inst.Score += itemScore;
            Destroy(this.gameObject);
        }
    }
}
