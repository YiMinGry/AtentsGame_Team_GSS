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

        // 콜라이더 방향으로 오락기 방향 구분. 말풍선 모양 바꿈
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

            //var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);//월드 좌표를 스크린 좌표로 vector3반환
            //if (screenPos.z < 0.0f)
            //{
            //    screenPos *= -1.0f; //z 는 메인카메라에서 xy평면까지의 거리 이처리를 안하면 반대편에서도 적의 hp만 보임
            //}
            //var localPos = Vector2.zero;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
            ////스크린포인트에서 ui캔버스의 좌표로 localPos에저장
            //rectImage.localPosition = localPos; //체력의 좌표를 localpos로
        }
    }
}
