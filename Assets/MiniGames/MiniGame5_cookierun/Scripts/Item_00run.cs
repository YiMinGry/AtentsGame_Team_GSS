using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_00run : MonoBehaviour
{
    public int itemScore = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager_00run.Inst.Score += itemScore;
            Destroy(this.gameObject);
        }
    }
}
