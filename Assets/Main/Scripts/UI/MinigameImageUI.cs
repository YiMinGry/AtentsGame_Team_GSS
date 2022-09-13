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

    private Image frameImg;
    private Image contentsImg;
    public Vector3 offset = Vector3.zero;

    public Sprite[] minigameSprites;
    public Sprite[] frameSprites = new Sprite[3];

    [HideInInspector] public Transform targetTr;

    bool isOpen = false;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectImage = GetComponent<RectTransform>();
        frameImg = transform.GetChild(0).GetComponentInChildren<Image>();
        contentsImg = frameImg.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();

        isOpen = false;
        frameImg.gameObject.SetActive(isOpen);
    }

    public void PopUpImage(Transform _targetTr, string _name)
    {
        int num = int.Parse(_name[_name.Length - 1].ToString()) -1;
        contentsImg.sprite=minigameSprites[num];

        targetTr = _targetTr;

        // �ݶ��̴� �������� ������ ���� ����. ��ǳ�� ��� �ٲ�
        float yAngle = targetTr.localEulerAngles.y;
        //Debug.Log($"arcade {_name} y = {yAngle}");

        if (yAngle < 10f) // y = 0
        {
            frameImg.sprite = frameSprites[1];
        } 
        else if (yAngle > 80f && yAngle < 100f) // y = 90
        {
            frameImg.sprite = frameSprites[2];
        } 
        else if (yAngle > 260f) // y = 270
        {
            frameImg.sprite = frameSprites[0];
        }


        isOpen = true;
        frameImg.gameObject.SetActive(isOpen);
    }

    public void ToggleUI()
    {
        isOpen = !isOpen;
        frameImg.gameObject.SetActive(isOpen);
    }
    private void LateUpdate()
    {
        if (targetTr != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(targetTr.position + offset);

            //var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);//���� ��ǥ�� ��ũ�� ��ǥ�� vector3��ȯ
            //if (screenPos.z < 0.0f)
            //{
            //    screenPos *= -1.0f; //z �� ����ī�޶󿡼� xy�������� �Ÿ� ��ó���� ���ϸ� �ݴ��������� ���� hp�� ����
            //}
            //var localPos = Vector2.zero;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
            ////��ũ������Ʈ���� uiĵ������ ��ǥ�� localPos������
            //rectImage.localPosition = localPos; //ü���� ��ǥ�� localpos��
        }
    }
}
