using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FEvent : MonoBehaviour
{
    public GameObject info;
    public Text infoText;
    public GameObject dim;

    public Dictionary<string, GameObject> lobbyUis = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Regist("ActiveFInfo", ActiveFInfo);
        EventManager.Regist("DeActiveFInfo", DeActiveFInfo);

        EventManager.Regist("OpenUI", OpenUI);
        EventManager.Regist("CloseUI", CloseUI);

        lobbyUis.Add("Ranking", dim.transform.Find("Ranking").gameObject);
        lobbyUis.Add("Friends", dim.transform.Find("Friends").gameObject);

    }

    public void ActiveFInfo(string _str)
    {
        info.SetActive(true);
        infoText.text = _str;
    }
    public void DeActiveFInfo(string _str = "")
    {
        info.SetActive(false);
    }

    public void OpenUI(string _str)
    {
        dim.SetActive(true);
        lobbyUis[_str].SetActive(true);

    }

    public void CloseUI(string _str)
    {
        dim.SetActive(false);
        lobbyUis[_str].SetActive(false);
    }

}
