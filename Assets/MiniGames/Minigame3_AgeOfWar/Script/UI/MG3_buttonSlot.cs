using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MG3_buttonSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
   
    
    
    public void OnPointerEnter(PointerEventData eventData)      //버튼 엔터될떄 글자 띄우는 이벤트
    {
        if(id<3)
        {
            MG3_UnitData unitData = MG3_GameManager.Inst.UnitDataMgr[id + 3 * MG3_GameManager.Inst.Revolution];
            MG3_GameManager.Inst.Menu.Detail.Expose(unitData);
        }
        else if(id<4)
        {
            MG3_TurretData turretData=MG3_GameManager.Inst.TurretDataMgr[MG3_GameManager.Inst.Revolution];
            MG3_GameManager.Inst.Menu.Detail.Expose(turretData);
        }
        else
        {
            MG3_GameManager.Inst.Menu.Detail.ExposeDetail(id);
        }
        //else if(id<5)
        //{
        //   GameManager.Inst.Menu.Detail.ExposeAddSlotCost();
        //}
        //else if(id<6)
        //{
        //    GameManager.Inst.Menu.Detail.ExposeSellTurret();
        //}
        //else if (id < 7)
        //{
        //    GameManager.Inst.
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MG3_GameManager.Inst.Menu.Detail.hide();
    }
}

