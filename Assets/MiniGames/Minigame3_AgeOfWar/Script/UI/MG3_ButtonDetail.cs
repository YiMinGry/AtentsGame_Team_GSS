using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MG3_ButtonDetail : MonoBehaviour
{
    int id;
    Text textDetail;
    private void Awake()
    {
        textDetail=GetComponent<Text>();
    }

    public void Expose(MG3_UnitData _unitdata)
    {
        textDetail.text = $"{_unitdata.UnitName} - ${_unitdata.cost}";
    }
    public void Expose(MG3_TurretData turretData)
    {
        textDetail.text = $"{turretData.turretName} - ${turretData.cost}";
    }
    public void ExposeAddSlotCost()
    {
        
    }
    public void ExposeSellTurret(MG3_TurretData _turretData)
    {

        textDetail.text = $"Sell {_turretData.turretName} ${_turretData.cost / 2}  ";
    }
    public void ExposeSellTurret()
    {
       
    }
    public void ExposeDetail(int id)
    {
        if(id==4)
        {
            if (MG3_GameManager.Inst.NumTurretSlot > 1)
            {
                textDetail.text = "No more Add Slot";
            }
            else
            {
                textDetail.text = $"Add Turret Slot - ${MG3_GameManager.Inst.AddSlotCost}";
            }
        }
        if(id==5)
        {
            textDetail.text = "Sell Turret";
        }
        if(id==6)
        {
            textDetail.text = "Spawn Unit";
        }
        if (id == 7)
        {
            textDetail.text = "Build Turret";
        }
        if(id==8)
        {
            if (MG3_GameManager.Inst.Revolution < 2)
            {
                if (MG3_GameManager.Inst.Exp >= MG3_GameManager.Inst.RevolExps[MG3_GameManager.Inst.Revolution])
                {
                    textDetail.text = "Revolution";
                }
                else
                {
                    textDetail.text = $"{MG3_GameManager.Inst.RevolExps[MG3_GameManager.Inst.Revolution]}EXP Required";
                }
            }
            else
            {
                textDetail.text = "No more Revolution";
            }
               
        }
    }
    public void hide()
    {
        textDetail.text = "";
    }
   
}
