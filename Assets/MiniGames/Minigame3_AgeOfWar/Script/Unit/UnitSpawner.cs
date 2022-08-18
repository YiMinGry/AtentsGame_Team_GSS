using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    //public GameObject unitPrefab;
    protected int unitCount = 0;
    protected bool isEnemy = false;
    protected GameObject[,] units = new GameObject[3, 4];
    protected int revolNum = 0;

    virtual protected void Start()
    {
    //    for(int i=0;i<3;i++)                                                
    //    {
    //        GameObject unitType = unitPrefab.transform.GetChild(i).gameObject;  //i·Î À¯´Ö Å¸ÀÔÀ» ³ª´® 
    //        for(int j=0;j<3;j++)
    //        {
    //            units[i, j] = unitType.transform.GetChild(j).gameObject;        //j·Î À¯´Ö ÁøÈ­º° ³ª´®
    //        }
    //    }
    }

    
    
    public void SpawnUnit(int unitType)
    {
        revolNum = GameManager.Inst.Revolution;
        UnitData unitData = GameManager.Inst.UnitDataMgr[unitType];
        //Unit unit = units[unitType, revolNum].GetComponent<Unit>();
        //Unit unit = unitData.UnitPrefab.GetComponent<Unit>();
        //unit.SetUnitStat(GameManager.Inst.UnitDataMgr[unitType+revolNum*3]);
        //unit.SetUnitStat(unitData);


        if (GameManager.Inst.Gold >= unitData.cost)
        {
            GameObject unitObject = Instantiate(unitData.UnitPrefab, transform);
            Unit unit = unitObject.GetComponent<Unit>();
            unit.SetUnitStat(unitData);
            unit.UnitNum = unitCount;
            if (isEnemy)
            {
                unitObject.tag = "Enemy";
            }
            else
            {
                GameManager.Inst.Gold -= unit.Cost;
            }
            unitCount++;
        }
    }
}