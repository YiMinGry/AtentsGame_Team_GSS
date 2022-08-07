using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public GameObject turretSlotObj;
    private TurretSlot turretSlot;
    private Image slotImg;

    private void Start()
    {
        slotImg = GetComponent<Image>();
        turretSlot = turretSlotObj.GetComponent<TurretSlot>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) //좌클릭
        {

            TempTurretImage temp = GameManager.Inst.Menu.TempTrImage;
            int gold = GameManager.Inst.Gold;
            int revolNum = GameManager.Inst.Revolution;
            
            if (GameManager.Inst.Menu.isSellTurret && turretSlot.TurretData != null)
            {
                turretSlot.SellTurret();
                slotImg.color = Color.white;
                GameManager.Inst.Menu.isSellTurret = false;
                GameManager.Inst.Menu.OnClickTurretCancel?.Invoke();
            }
            else if (temp.IsExpose() && slotImg.color != Color.clear && !GameManager.Inst.Menu.isSellTurret&&
                gold >= GameManager.Inst.TurretDataMgr[revolNum].cost)
            {

                turretSlot.SetTurret(GameManager.Inst.Revolution);
                GameManager.Inst.Menu.OnClickTurretCancel?.Invoke();//temp를 hide하고 Cancel창 지움
                slotImg.color = Color.clear;
                //gameObject.SetActive(false);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Inst.Menu.isSellTurret && turretSlot.TurretData != null)
        {
            GameManager.Inst.Menu.Detail.ExposeSellTurret(turretSlot.TurretData);
        }
    }


    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(turretSlotObj.transform.position);
    }
}

   
