using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawn : MonoBehaviour
{
    private GameObject[] pet = new GameObject[3];

    private int petNum = 3; // 플레이어가 장착한 펫 개수

    void Start()
    {
        for (int i = 0; i < petNum; i++)
        {
            pet[i] = ResourceManager.Inst.Instantiate($"Pets/Pet{i + 1}", this.transform); // Resources/Prefabs/Pets 경로에 있는 Pet1~ prefab 생성
            pet[i].transform.position = pet[i].transform.forward * (-0.5f)*(i+1) + pet[i].transform.up*(0.3f)*(i+1); // 펫 위치 조정
        }
    }

    // Update is called once per frame

}
