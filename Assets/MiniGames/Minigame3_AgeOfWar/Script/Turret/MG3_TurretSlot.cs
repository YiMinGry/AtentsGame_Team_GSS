using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_TurretSlot : MonoBehaviour
{
    MG3_TurretData turretData;
    GameObject turretObj;
    public MG3_TurretData TurretData => turretData;
    public void SetTurret(int _id)
    {
        
        turretData = MG3_GameManager.Inst.TurretDataMgr[_id];
        MG3_GameManager.Inst.Gold -= turretData.cost;
        turretObj =Instantiate(turretData.turretPrefab,transform).gameObject;
    }
    public void SellTurret()
    {
        MG3_GameManager.Inst.Gold += turretData.cost/2;
        turretData = null;
        Destroy(turretObj);
        

    }
}
