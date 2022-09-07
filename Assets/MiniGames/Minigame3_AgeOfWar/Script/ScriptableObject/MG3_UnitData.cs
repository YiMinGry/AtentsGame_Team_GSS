using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data", order = 1)]
public class MG3_UnitData :ScriptableObject
{
    public uint id = 0;
    public string UnitName = "»õ À¯´Ö";
    public GameObject UnitPrefab;
    public int attack;
    public int hpMax;
    public int cost;
    public int exp;
    public float buildTime;
    public float meleeTime;
    public float rangeTime;


}
