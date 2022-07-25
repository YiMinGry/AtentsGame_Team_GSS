using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    protected int unitCount = 0;
    protected bool isEnemy = false;
    protected GameObject[,] units = new GameObject[3, 4];
    protected int revolNum = 0;

    virtual protected void Start()
    {
        for(int i=0;i<3;i++)                                                
        {
            GameObject unitType = unitPrefab.transform.GetChild(i).gameObject;  //i·Î À¯´Ö Å¸ÀÔÀ» ³ª´® 
            for(int j=0;j<4;j++)
            {
                units[i, j] = unitType.transform.GetChild(j).gameObject;        //j·Î À¯´Ö ÁøÈ­º° ³ª´®
            }
        }
    }

    
    public void SpawnMeleeUnit()
    {
        SpawnUnit(0);
    }
    public void SpawnRangeUnit()
    {
        SpawnUnit(1);
    }
    public void SpawnEliteUnit()
    {
        SpawnUnit(2);
    }
    public void SpawnUnit(int unitType)
    {
        revolNum = GameManager.Inst.Revolution;
        Unit unit = units[unitType, revolNum].GetComponent<Unit>();
        unit.SetUnitStat(GameManager.Inst.UnitDataMgr[unitType+revolNum*3]);
        //switch(unitType)
        //{
        //    case 0:
        //        switch (GameManager.Inst.Revolution)
        //        {
        //            case 0:
        //                unit.SetUnitStat(5, 20, 15, 20, 3.0f, 1.3f, 1.0f);
        //                break;
        //            case 1:
        //                unit.SetUnitStat(10, 50, 100, 50, 3.0f, 1.3f, 1.0f);
        //                break;
        //            default:
        //                break;
        //        }
        //        break;
        //    case 1:
                
        //        switch (GameManager.Inst.Revolution)
        //        {
        //            case 0:
        //                unit.SetUnitStat(3, 15, 30, 20, 3.0f, 1.3f, 1.0f);
        //                break;
        //            case 1:
        //                unit.SetUnitStat(10, 40, 200, 20, 3.0f, 1.3f, 1.0f);
        //                break;
        //            default:
        //                break;
        //        }
        //        break;
        //    case 2:
        //        switch (GameManager.Inst.Revolution)
        //        {
        //            case 0:
        //                unit.SetUnitStat(10, 100, 200, 200, 3.0f, 1.3f, 1.0f);
        //                break;
        //            case 1:
        //                unit.SetUnitStat(20, 200, 100, 20, 3.0f, 1.3f, 1.0f);
        //                break;
        //            default:
        //                break;
        //        }
        //        break;
        //    default:
        //        break;
        //}
        
        if (GameManager.Inst.Gold >= unit.Cost)
        {
            GameObject unitObject = Instantiate(units[unitType, revolNum], transform);
            unitObject.GetComponent<Unit>().UnitNum = unitCount;
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