using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll_00run : MonoBehaviour
{
    public Vector3 scrollSpeed = Vector3.zero;
    public bool isStop = false;

    private void Update()
    {
        if (isStop == false)
            transform.Translate(scrollSpeed * Time.deltaTime, Space.Self);
    }
}
