using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MG3_TempTurretImage : MonoBehaviour
{
    public Image image;
    private bool isMove = false;
    
    private void Awake()
    {
        image= GetComponent<Image>();
        
    }
    private void Start()
    {
        
        MG3_GameManager.Inst.Menu.OnClickTurretButton += Expose;
        MG3_GameManager.Inst.Menu.OnClickTurretCancel += hide;
    }
   
    public void Expose()
    {
        Color color = Color.white;
        color.a = 0.5f;
        image.color = color;
        image.sprite = MG3_GameManager.Inst.TurretDataMgr[MG3_GameManager.Inst.Revolution].turretIcon;
        isMove = true;
    }
    public void hide()
    {
        image.color = Color.clear;
        isMove = false;
    }

    public bool IsExpose()
    {
        return image.color != Color.clear;
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
