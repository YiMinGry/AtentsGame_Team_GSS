using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlot : MonoBehaviour
{
    TurretData turretData;
    GameObject turretObj;
    public TurretData TurretData => turretData;
    public void SetTurret(int _id)
    {
        
        turretData = GameManager.Inst.TurretDataMgr[_id];
        GameManager.Inst.Gold -= turretData.cost;
        turretObj =Instantiate(turretData.turretPrefab,transform).gameObject;
    }
    public void SellTurret()
    {
        GameManager.Inst.Gold += turretData.cost/2;
        turretData = null;
        Destroy(turretObj);
        

    }
}
