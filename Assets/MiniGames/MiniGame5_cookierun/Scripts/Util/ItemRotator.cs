using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public float rotateSpeed = 360.0f;
    public float moveRange = 0.3f;

    float timeElapsed = 0;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        transform.position = startPos + new Vector3(0, (1 - Mathf.Cos(timeElapsed)) * moveRange, 0);

        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }
}
