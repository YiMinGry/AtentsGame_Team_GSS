using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_TurretDataManager : MonoBehaviour
{
    public MG3_TurretData[] turretDatas;
    public MG3_TurretData this[int i] => turretDatas[i];
    public int Length => turretDatas.Length;
}
