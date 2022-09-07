using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public float rotateSpeed = 360.0f;
    public float moveRange = 0.3f;
    public bool isRotate = true;

    private void Update()
    {
        if (isRotate)
            transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }
}
