using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_FollowingTarget : MonoBehaviour
{

    public Transform target;
    public bool setTargetParent = false;
    public Vector3 dis = Vector3.zero;

    private void Start()
    {
        Init();
    }

    public void Init(Transform newTarget = null)
    {
        target = newTarget;
        if (target == null)
        {
            target = MiniGame5_GameManager.Inst.Player.transform;
        }

        if (setTargetParent) transform.parent = target;
    }

    private void Update()
    {
        if (!setTargetParent)
        {
            Vector3 fixedPos = new Vector3(target.position.x + dis.x,
                                           target.position.y + dis.y,
                                           target.position.z + dis.z);
            transform.position = fixedPos;
        }
    }
}
