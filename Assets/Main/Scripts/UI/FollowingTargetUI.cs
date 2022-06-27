using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowingTargetUI : MonoBehaviour
{
    public Transform target;
    public Vector3 UIPosition = Vector3.zero;

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + UIPosition);
    }
}
