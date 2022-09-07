using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Turret Data", menuName = "Scriptable Object/Turret Data", order = 1)]

public class MG3_TurretData : ScriptableObject
{
    public uint id = 0;
    public string turretName = "�� �ͷ�";
    public GameObject turretPrefab;
    public int attack;
    public int cost;
    public float attackTime;
    public Sprite turretIcon;
}
