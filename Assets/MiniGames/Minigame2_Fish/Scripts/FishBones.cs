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
        for (int i = transform.childCount; i > 0; i--) // HP�� ���� FishBone Ȱ��ȭ
        {
            if (i > MG2_GameManager.Inst.HealthPoint) // i�� HP���� ũ�� fishBone ��Ȱ��ȭ
            {
                fishBones[i - 1].enabled = false;
            }
            else // i�� HP���� �۰ų� ������ fishBone ��Ȱ��ȭ
            {
                fishBones[i - 1].enabled = true;
            }
        }
        if (MG2_GameManager.Inst.HealthPoint < transform.childCount) // HP�� �������� ���� ����
        { 
            Vector3 _pos = Camera.main.ScreenToWorldPoint(fishBones[MG2_GameManager.Inst.HealthPoint].transform.position // ĵ������ FishBone�� ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
                + new Vector3(0, 0, 7)); // z�� ��ġ ����(-10 + 7 = 3)
            MG2_GameManager.Inst.mg2_EffectManager.MakeHPLossEffect(_pos);
        }

    }

}
