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
        for(int i = transform.childCount; i > 0; i--)
        {
            if(i > MG2_GameManager.Inst.HealthPoint)
            {
                fishBones[i-1].enabled = false;
            }
            else
            {
                fishBones[i-1].enabled = true;
            }
        }        
    }

}
