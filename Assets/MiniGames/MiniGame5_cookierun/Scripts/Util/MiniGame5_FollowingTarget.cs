using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_FollowingTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 dis = Vector3.zero;
    //public Vector3 dir = Vector3.zero;
    public float size = 1;

    private void Start()
    {
        if (target == null)
        {
            target = MiniGame5_GameManager.Inst.Player.transform;
        }
        transform.parent = target;
        transform.localPosition = dis;
        //transform.localRotation = Quaternion.Euler(dir);
        transform.localScale = size * Vector3.one;
    }
}
