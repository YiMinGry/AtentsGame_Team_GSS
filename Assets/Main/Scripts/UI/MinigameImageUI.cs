using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameImageUI : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectImage;
    private Image image;
    public Vector3 offset = Vector3.zero;
    public Sprite[] minigameSprites;
    [HideInInspector] public Transform targetTr;
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectImage = GetComponent<RectTransform>();
        image = GetComponentInParent<Image>();
    }

    public void PopUpImage(Transform _targetTr,string _name)
    {
        int num = int.Parse(_name[_name.Length - 1].ToString()) -1;
        image.sprite=minigameSprites[num];

        targetTr = _targetTr;
        image.enabled = true;

    }
    public void HideImage()
    {
        image.enabled = false;
    }
    private void LateUpdate()
    {
        if (targetTr != null)
        {
            var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);//���� ��ǥ�� ��ũ�� ��ǥ�� vector3��ȯ
            if (screenPos.z < 0.0f)
            {
                screenPos *= -1.0f; //z �� ����ī�޶󿡼� xy�������� �Ÿ� ��ó���� ���ϸ� �ݴ������� ���� hp�� ����
            }
            var localPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
            //��ũ������Ʈ���� uiĵ������ ��ǥ�� localPos������
            rectImage.localPosition = localPos; //ü���� ��ǥ�� localpos��
        }
    }
}
