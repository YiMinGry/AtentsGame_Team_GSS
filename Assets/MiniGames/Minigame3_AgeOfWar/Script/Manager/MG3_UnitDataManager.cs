using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_UnitDataManager : MonoBehaviour
{
    public MG3_UnitData[] unitDatas;
    public MG3_UnitData this[int i] => unitDatas[i];
    public int Length => unitDatas.Length;
}
