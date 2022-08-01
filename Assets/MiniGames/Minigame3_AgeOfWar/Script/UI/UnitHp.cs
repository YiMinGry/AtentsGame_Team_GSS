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
            var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);//���� ��ǥ�� ��ũ�� ��ǥ�� vector3��ȯ
            if (screenPos.z < 0.0f)
            {
                screenPos *= -1.0f; //z �� ����ī�޶󿡼� xy�������� �Ÿ� ��ó���� ���ϸ� �ݴ������� ���� hp�� ����
            }
            var localPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
            //��ũ������Ʈ���� uiĵ������ ��ǥ�� localPos������
            rectHp.localPosition = localPos; //ü���� ��ǥ�� localpos��
        }
        

    }
}
