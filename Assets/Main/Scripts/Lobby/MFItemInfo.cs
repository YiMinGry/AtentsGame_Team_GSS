using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MFItemInfo : MonoBehaviour
{
    Callback _cb;

    MiniFriendData data;

    [SerializeField]
    Image _img;//����Ͽ� �⺻ ? �̹���  �����ϴ� �������� �ش� ����Ϸ� ����
    [SerializeField]
    GameObject _active;//Ȱ��ȭ ����
    GameObject page2_MiniDetail_Check;
    GameObject page2_MiniDetail_NotCheck;

    public void Init(ref MiniFriendData _data, GameObject _Check, GameObject _NotCheck)//����Ʈ�� �̴�ģ�� ���� ����
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

    public void OnClick_MF() //�̴�ģ�� ������ �������� ��
    {
        if (_cb != null)
        {
            _cb.Invoke();
        }
    }


}
