using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawn : MonoBehaviour
{
    private GameObject[] pet = new GameObject[3];

    private int petNum = 3; // �÷��̾ ������ �� ����

    void Start()
    {
        for (int i = 0; i < petNum; i++)
        {
            pet[i] = ResourceManager.Inst.Instantiate($"Pets/Pet{i + 1}", this.transform); // Resources/Prefabs/Pets ��ο� �ִ� Pet1~ prefab ����
            pet[i].transform.position = pet[i].transform.forward * (-0.5f)*(i+1) + pet[i].transform.up*(0.3f)*(i+1); // �� ��ġ ����
        }
    }

    // Update is called once per frame

}
