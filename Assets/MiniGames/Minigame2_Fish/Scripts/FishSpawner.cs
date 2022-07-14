using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    float heigthMin = -5.0f;
    float heigthMax = 5.0f;

    public GameObject[] enemyFish = null; // 0 ~ 5 �� 1 ~ 6 ���� ����� ���� ��

    public float spawnInterval = 1.0f; // ����� ��������

    WaitForSeconds waitSecond;

    private void Start()
    {
        waitSecond = new WaitForSeconds(spawnInterval);
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true) 
        {
            yield return waitSecond; 
            GameObject obj = Instantiate(enemyFish[Random.Range(0,MG2_GameManager.Inst.Stage + 1)]); // 1 ~ �������� ���� ���� ����� ����
            obj.transform.position = this.transform.position; // ������ ��ġ�� ����
            obj.transform.Translate(Vector3.up * Random.Range(heigthMin, heigthMax)); // ������ ��ġ���� ���� �����ϰ� ��ġ ����
            if (this.transform.position.x < 0) // ���� �����ʿ����� ����Ⱑ �ݴ� �������� ������ ����
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z * (-1.0f));
            }
        }
    }

}
