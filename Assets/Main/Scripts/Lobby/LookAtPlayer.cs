using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;

    [Space(10)]
    public bool isFollowingPlayerMode = false;
    public bool isFpsMode = false;

    [Header("Following Player_FPS Mode")]
    public float dist = 4f;
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;
    private float x = 0.0f;
    private float y = 0.0f;

    [Header("Following Player_Fixed Angle Mode")] //카메라 거리 조절용
    [Range(-1.0f, 6.0f)]
    public float yAngle = 1.5f;
    [Range(1.5f, 6.0f)]
    public float zAngle = 1.5f;

    [Header("Following Player_Phone Mode")]
    public float animDuration = 0.5f;
    public Ease animEase = Ease.OutCubic;
    Vector3 originPosition = Vector3.zero;
    Quaternion originRotation = Quaternion.Euler(Vector3.zero);

    private void Awake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
    }

    private void Start()
    {
        if (isFollowingPlayerMode == false)
        {
            originPosition = new Vector3(0.0f, 2.6f, 11.0f);
            originRotation = Quaternion.Euler(new Vector3(20.0f, 180.0f, 0.0f));
            transform.position = originPosition;
            transform.rotation = originRotation;
        }
    }

    void Update()
    {
        if (isFollowingPlayerMode == true)
        {
            if (isFpsMode == true)
            {
                //마우스로 시점 조정
                FollowingPlayer_FPSMode();
            }
            else
            {
                //고정 시점
                FollowingPlayer_FixedAngle();
            }
        }
    }

    private void FollowingPlayer_FPSMode()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;


            if (target)
            {
                dist -= 1 * Input.mouseScrollDelta.y;

                if (dist < 0.5f)
                {
                    dist = 1;
                }

                if (dist >= 9)
                {
                    dist = 9;
                }

                x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

                if (y < 3)
                {
                    y = 3;
                }

                if (y > 85)
                {
                    y = 85;
                }

                originRotation = Quaternion.Euler(y, x, 0);
                originPosition = originRotation * new Vector3(0.0f, 0.0f, -dist) + target.position + new Vector3(0.0f, 1, 0.0f);

                transform.rotation = originRotation;
                transform.position = originPosition;
            }
        }
    }

    bool isPlayerOnEgdeOfMap = false;

    private void FollowingPlayer_FixedAngle()
    {
        Cursor.lockState = CursorLockMode.Confined;

        //플레이어가 맵끝에 있는지 체크
        if (target.position.z < 8.3f)
            isPlayerOnEgdeOfMap = false;
        else
            isPlayerOnEgdeOfMap = true;

        //카메라 앵글
        if (isPlayerOnEgdeOfMap == false)
            originPosition = new Vector3(target.position.x, target.position.y + yAngle, target.position.z + zAngle);
        else
            originPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

        transform.position = originPosition;
        originRotation = transform.rotation;

        if (isPlayerOnEgdeOfMap == false)
            transform.LookAt(target);
    }

    public IEnumerator doMove_PhoneModeOn(Transform PlayerTrs, Transform TargetCamTrs)
    {
        isFollowingPlayerMode = false;
        transform.DOMove(PlayerTrs.position + TargetCamTrs.localPosition, animDuration).SetEase(animEase);
        transform.DORotate(TargetCamTrs.localRotation.eulerAngles, animDuration).SetEase(animEase);
        yield return new WaitForSeconds(animDuration);
    }

    public IEnumerator doMove_PhoneModeOff()
    {
        transform.DOMove(originPosition, animDuration).SetEase(animEase);
        transform.DORotate(originRotation.eulerAngles, animDuration).SetEase(animEase).OnComplete(() => { isFollowingPlayerMode = true; });
        yield return null;
        //yield return new WaitForSeconds(animDuration);
        //isFollowingPlayerMode = true;
    }
}
