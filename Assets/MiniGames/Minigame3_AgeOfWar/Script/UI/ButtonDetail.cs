using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDetail : MonoBehaviour
{
    int id;
    Text textDetail;
    private void Awake()
    {
        textDetail=GetComponent<Text>();
    }

    public void Expose(UnitData _unitdata)
    {
        textDetail.text = $"{_unitdata.UnitName} - ${_unitdata.cost}";
    }
    public void Expose(TurretData turretData)
    {
        textDetail.text = $"{turretData.turretName} - ${turretData.cost}";
    }
    public void ExposeAddSlotCost()
    {
        if(GameManager.Inst.NumTurretSlot>1)
        {
            textDetail.text = "No more Add Slot";
        }
        else
        {
            textDetail.text = $"Add Turret Slot - ${GameManager.Inst.AddSlotCost}";
        }
    }
    public void ExposeSellTurret(TurretData _turretData)
    {
        
        textDetail.text = $"Sell {_turretData.turretName} ${_turretData.cost/2}  ";
    }
    public void ExposeSellTurret()
    {
        textDetail.text = "Sell Turret";
    }

    public void hide()
    {
        textDetail.text = "";
    }
   
}
