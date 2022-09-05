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
            var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);//월드 좌표를 스크린 좌표로 vector3반환
            if (screenPos.z < 0.0f)
            {
                screenPos *= -1.0f; //z 는 메인카메라에서 xy평면까지의 거리 이처리를 안하면 반대편에서도 적의 hp만 보임
            }
            var localPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
            //스크린포인트에서 ui캔버스의 좌표로 localPos에저장
            rectImage.localPosition = localPos; //체력의 좌표를 localpos로
        }
    }
}
