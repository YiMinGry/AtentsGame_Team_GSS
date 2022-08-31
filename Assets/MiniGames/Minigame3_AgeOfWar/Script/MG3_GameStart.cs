using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG3_GameStart : MonoBehaviour
{
    Button[] buttons=new Button[2];
    Image image;
    public System.Action OnClickStart;
    private void Awake()
    {
        for(int i=0;i<buttons.Length;i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<Button>();
        }
        image= GetComponent<Image>();
        
       
    }
    private void Start()
    {
        OnClickStart += CloseStartImg;
        buttons[0].onClick.AddListener(ClickStart);
    }
    void CloseStartImg()
    {
        gameObject.SetActive(false);
    }
    void ClickStart()
    {
        OnClickStart?.Invoke();
    }

}
