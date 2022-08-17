using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_MapScroll : MonoBehaviour
{
    public Vector3 scrollSpeed = Vector3.zero;
    public bool isStop = false;

    private void Update()
    {
        if (isStop == false)
            transform.Translate(scrollSpeed * Time.deltaTime, Space.Self);
    }
}
