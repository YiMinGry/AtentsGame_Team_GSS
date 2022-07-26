using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataManager : MonoBehaviour
{
    public UnitData[] unitDatas;
    public UnitData this[int i] => unitDatas[i];
    public int Length => unitDatas.Length;
}
