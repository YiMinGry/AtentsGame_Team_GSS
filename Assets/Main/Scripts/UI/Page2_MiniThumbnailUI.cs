using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page2_MiniThumbnailUI : MonoBehaviour
{
    public Sprite questionImg;

    public MiniFriendData data;

    Image miniImg;
    GameObject check;
    Button btn;

    private void Awake()
    {
        miniImg = GetComponent<Image>();
        
        check = transform.GetChild(0).gameObject;
        check.SetActive(false);
        
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SetDetail);

        if (data != null)
        {
            SetData();
        } else
        {
            miniImg.sprite = questionImg;
        }
    }

    public void SetData()
    {
        miniImg.sprite = data.sprite;
        if (data.isChoose == true) check.SetActive(true);
    }

    void SetDetail()
    {
        FindObjectOfType<PhoneUI>().SetPage2_detail(data);
    }
}
