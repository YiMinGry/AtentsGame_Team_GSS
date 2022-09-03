using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MFItemInfo : MonoBehaviour
{
    Callback _cb;

    MiniFriendData data;

    [SerializeField]
    Image _img;//썸네일용 기본 ? 이미지  소유하는 시점에서 해당 썸네일로 변경
    [SerializeField]
    GameObject _active;//활성화 여부
    GameObject page2_MiniDetail_Check;
    GameObject page2_MiniDetail_NotCheck;

    public void Init(ref MiniFriendData _data, GameObject _Check, GameObject _NotCheck)//리스트에 미니친구 정보 세팅
    {
        data = _data;
        gameObject.SetActive(true);
        name = "MiniF_" + data.id;
        _active.SetActive(data.isChoose == true);

        if (data.isHave == true)
        {
            _img.sprite = data.sprite;
        }

        page2_MiniDetail_Check = _Check;
        page2_MiniDetail_NotCheck = _NotCheck;
        EventManager.Regist("RefreshMF_" + data.id, Refresh);
    }


    public void Refresh(string _str = "")
    {
        _active.SetActive(data.isChoose == true);

        if (data.isHave == true)
        {
            _img.sprite = data.sprite;
        }

        page2_MiniDetail_Check.SetActive(data.isChoose);

        page2_MiniDetail_NotCheck.SetActive(!data.isChoose);
    }

    public void SetCallBack(Callback _callback)
    {
        if (_cb != null)
        {
            _cb = null;
        }
        _cb = _callback;
    }

    public void OnClick_MF() //미니친구 눌러서 정보보는 용
    {
        if (_cb != null)
        {
            _cb.Invoke();
        }
    }


}
