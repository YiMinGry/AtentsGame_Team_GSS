using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        upDownCheck -= Time.deltaTime;
        if(upDownCheck < 0)
        {
            upDownCheck = 1.0f;
            upDownSpeed *= -1.0f;
        }
        UpDown();
        Rotate();
    }

    float upDownSpeed = 50.0f;
    float upDownCheck = 1.0f;

    private void UpDown()
    {
        rect.position += new Vector3(0, upDownSpeed * Time.deltaTime,0);
    }

    float angle = 0.0f;
    float rotateSpeed = 180.0f;
    private void Rotate()
    {
        angle += rotateSpeed * Time.deltaTime;
        rect.rotation = Quaternion.Euler(0, angle, -90);
    }

    
}
