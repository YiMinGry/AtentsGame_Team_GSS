using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public float endTime = 0;
    void Start()
    {
        Destroy(gameObject, endTime);
    }

}
