using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishBones : MonoBehaviour
{
    Image[] fishBones;

    private void Awake()
    {
        fishBones = new Image[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            fishBones[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    private void Start()
    {
        MG2_GameManager.Inst.playerHPChange = playerHPChange;
    }

    void playerHPChange()
    {
        if (MG2_GameManager.Inst.HealthPoint > -1)
        {
            fishBones[MG2_GameManager.Inst.HealthPoint].enabled = false;
        }
    }

}
