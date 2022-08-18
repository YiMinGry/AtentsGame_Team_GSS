using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class buttonSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
   
    
    
    public void OnPointerEnter(PointerEventData eventData)      //��ư ���͵ɋ� ���� ���� �̺�Ʈ
    {
        if(id<3)
        {
            UnitData unitData = GameManager.Inst.UnitDataMgr[id + 3 * GameManager.Inst.Revolution];
            GameManager.Inst.Menu.Detail.Expose(unitData);
        }
        else if(id<4)
        {
            TurretData turretData=GameManager.Inst.TurretDataMgr[GameManager.Inst.Revolution];
            GameManager.Inst.Menu.Detail.Expose(turretData);
        }
        else
        {
            GameManager.Inst.Menu.Detail.ExposeDetail(id);
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
        GameManager.Inst.Menu.Detail.hide();
    }
}

