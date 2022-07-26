using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Turret Data", menuName = "Scriptable Object/Turret Data", order = 1)]

public class TurretData : ScriptableObject
{
    public uint id = 0;
    public string turretName = "ªı ≈Õ∑ø";
    public GameObject turretPrefab;
    public int attack;
    public int cost;
    public float attackTime;
}
