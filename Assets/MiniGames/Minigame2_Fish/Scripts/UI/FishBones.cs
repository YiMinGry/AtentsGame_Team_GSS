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
        for (int i = transform.childCount; i > 0; i--) // HP에 따라 FishBone 활성화
        {
            if (i > MG2_GameManager.Inst.HealthPoint) // i가 HP보다 크면 fishBone 비활성화
            {
                fishBones[i - 1].enabled = false;
            }
            else // i가 HP보다 작거나 같으면 fishBone 비활성화
            {
                fishBones[i - 1].enabled = true;
            }
        }
        if (MG2_GameManager.Inst.HealthPoint < transform.childCount) // HP가 감소했을 때만 실행
        { 
            Vector3 _pos = Camera.main.ScreenToWorldPoint(fishBones[MG2_GameManager.Inst.HealthPoint].transform.position // 캔버스의 FishBone의 스크린 좌표를 월드 좌표로 변환
                + new Vector3(0, 0, 7)); // z축 위치 보정(-10 + 7 = 3)
            MG2_GameManager.Inst.mg2_EffectManager.MakeHPLossEffect(_pos);
        }

    }

}
