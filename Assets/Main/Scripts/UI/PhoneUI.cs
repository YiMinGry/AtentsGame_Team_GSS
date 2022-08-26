using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : MonoBehaviour
{
    Button[] tabBtns;
    Transform[] pages;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        Transform tab = transform.GetChild(0).Find("Tabs");
        tabBtns = new Button[tab.childCount];
        for (int i = 0; i < tab.childCount; i++)
        {
            tabBtns[i] = tab.GetChild(i).GetComponent<Button>();
        }

        Transform page = transform.GetChild(0).Find("Pages");
        pages = new Transform[page.childCount];
        for (int i = 0; i < page.childCount; i++)
        {
            pages[i] = page.GetChild(i);
        }

        tabBtns[0].onClick.AddListener(() => OpenPage(0));
        tabBtns[1].onClick.AddListener(() => OpenPage(1));
        tabBtns[2].onClick.AddListener(() => OpenPage(2));
        tabBtns[3].onClick.AddListener(() => OpenPage(3));
    }

    void OpenPage(int index)
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }

        pages[index].gameObject.SetActive(true);
        Debug.Log($"open {index} page");
    }
}
