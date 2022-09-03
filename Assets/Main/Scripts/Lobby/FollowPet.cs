using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPet : MonoBehaviour
{
    public Transform target; // ���� Ÿ���� Ʈ���� ��

    public float dampSpeed = 2;


    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * dampSpeed);
        }
    }
}
