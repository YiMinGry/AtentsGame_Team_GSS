using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MG3_TurretSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject turretSlotObj;
    private MG3_TurretSlot turretSlot;
    private Image slotImg;

    private void Start()
    {
        slotImg = GetComponent<Image>();
        turretSlot = turretSlotObj.GetComponent<MG3_TurretSlot>();
        MG3_GameManager.Inst.Menu.OnClickTurretButton += MakeBright;
        MG3_GameManager.Inst.Menu.OnClickTurretCancel += MakeNoBright;
    }
    public void MakeBright()
    {
        if(slotImg.color!=Color.clear)
        {
            slotImg.color = Color.white;
        }
        
    }
    public void MakeNoBright()
    {
        if (slotImg.color != Color.clear)
        {
            slotImg.color = new Color(0.8f, 0.8f, 0.8f);
        }
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) //좌클릭
        {

            MG3_TempTurretImage temp = MG3_GameManager.Inst.Menu.TempTrImage;
            int gold = MG3_GameManager.Inst.Gold;
            int revolNum = MG3_GameManager.Inst.Revolution;
            
            if (MG3_GameManager.Inst.Menu.isSellTurret && turretSlot.TurretData != null)
            {
                turretSlot.SellTurret();
                slotImg.color = Color.white;
                MG3_GameManager.Inst.Menu.isSellTurret = false;
                MG3_GameManager.Inst.Menu.OnClickTurretCancel?.Invoke();
            }
            else if (temp.IsExpose() && slotImg.color != Color.clear && !MG3_GameManager.Inst.Menu.isSellTurret&&
                gold >= MG3_GameManager.Inst.TurretDataMgr[revolNum].cost)
            {

                turretSlot.SetTurret(MG3_GameManager.Inst.Revolution,true);
                MG3_GameManager.Inst.Menu.OnClickTurretCancel?.Invoke();//temp를 hide하고 Cancel창 지움
                slotImg.color = Color.clear;
                //gameObject.SetActive(false);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MG3_GameManager.Inst.Menu.isSellTurret && turretSlot.TurretData != null)
        {
            MG3_GameManager.Inst.Menu.Detail.ExposeSellTurret(turretSlot.TurretData);
        }
    }



    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(turretSlotObj.transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MG3_GameManager.Inst.Menu.isSellTurret && turretSlot.TurretData != null)
        {
            MG3_GameManager.Inst.Menu.Detail.hide();
        }
    }
}

   
