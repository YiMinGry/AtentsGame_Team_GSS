using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class buttonSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
  
    private void Awake()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(id<3)
        {
            UnitData unitData = GameManager.Inst.UnitDataMgr[id + 3 * GameManager.Inst.Revolution];
            GameManager.Inst.Menu.Detail.expose(unitData);
        }
        else
        {
            TurretData turretData=GameManager.Inst.TurretDataMgr[id-3+GameManager.Inst.Revolution];
            GameManager.Inst.Menu.Detail.expose(turretData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Inst.Menu.Detail.hide();
    }
}

