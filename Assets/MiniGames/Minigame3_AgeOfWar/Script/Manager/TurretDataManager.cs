using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDataManager : MonoBehaviour
{
    public TurretData[] turretDatas;
    public TurretData this[int i] => turretDatas[i];
    public int Length => turretDatas.Length;
}
