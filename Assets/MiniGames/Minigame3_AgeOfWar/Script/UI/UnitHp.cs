using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHp : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectHp;

    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Transform targetTr;
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = GetComponent<RectTransform>();
 
    }
    private void LateUpdate()
    {
        if(targetTr!=null)
        {
            var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);//월드 좌표를 스크린 좌표로 vector3반환
            if (screenPos.z < 0.0f)
            {
                screenPos *= -1.0f; //z 는 메인카메라에서 xy평면까지의 거리 이처리를 안하면 반대편에서도 적의 hp만 보임
            }
            var localPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
            //스크린포인트에서 ui캔버스의 좌표로 localPos에저장
            rectHp.localPosition = localPos; //체력의 좌표를 localpos로
        }
        

    }
}
