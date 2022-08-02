using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obastacle_00run : MonoBehaviour
{
    public MapScroll_00run map;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            map.isStop = true;
            collision.transform.GetComponent<Player_00run>().Die();
        }
    }
}
