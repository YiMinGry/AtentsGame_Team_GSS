using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    float heigthMin = -5.0f;
    float heigthMax = 5.0f;

    public GameObject[] enemyFish = null; // 0 ~ 5 �� 1 ~ 6 ���� ����� ���� ��

    [SerializeField]
    float spawnInterval = 1.0f; // ����� ��������

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
            GameObject obj = Instantiate(RandomEnemySpawn(MG2_GameManager.Inst.Stage)); // 1 ~ �������� ���� ���� ����� ����
            obj.transform.position = this.transform.position; // ������ ��ġ�� ����
            obj.transform.Translate(Vector3.up * Random.Range(heigthMin, heigthMax)); // ������ ��ġ���� ���� �����ϰ� ��ġ ����
            if (this.transform.position.x < 0) // ���� �����ʿ����� ����Ⱑ �ݴ� �������� ������ ����
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z * (-1.0f));
            }
        }
    }

    private GameObject RandomEnemySpawn(int stage) // ���������� ����� ���� Ȯ��
    {
        float rand = Random.Range(0.0f, 1.0f);
        switch (stage)
        {
            case 1:                     // �������� 1 : 40%, 60%
                if(rand < 0.4f)
                {
                    return enemyFish[0];
                }
                else
                {
                    return enemyFish[1];
                }
            case 2:                     // �������� 2 : 20%, 40%, 40%
                if (rand < 0.2f)
                {
                    return enemyFish[0];
                }
                else if(rand < 0.6f)
                {
                    return enemyFish[1];
                }
                else
                {
                    return enemyFish[2];
                }
            case 3:                     // �������� 3 : 10%, 30%, 30%, 30%
                if (rand < 0.1f)
                {
                    return enemyFish[0];
                }
                else if (rand < 0.4f)
                {
                    return enemyFish[1];
                }
                else if(rand < 0.7f)
                {
                    return enemyFish[2];
                }
                else
                {
                    return enemyFish[3];
                }
            case 4:                     // �������� 4 : 10%, 10%, 20%, 30%, 30%
                if (rand < 0.1f)
                {
                    return enemyFish[0];
                }
                else if (rand < 0.2f)
                {
                    return enemyFish[1];
                }
                else if (rand < 0.4f)
                {
                    return enemyFish[2];
                }
                else if (rand < 0.7f)
                {
                    return enemyFish[3];
                }
                else
                {
                    return enemyFish[4];
                }
            case 5:                     // �������� 5 : 5%, 5%, 10%, 30%, 30%, 20%
                if (rand < 0.05f)
                {
                    return enemyFish[0];
                }
                else if (rand < 0.1f)
                {
                    return enemyFish[1];
                }
                else if (rand < 0.2f)
                {
                    return enemyFish[2];
                }
                else if (rand < 0.3f)
                {
                    return enemyFish[3];
                }
                else if (rand < 0.4f)
                {
                    return enemyFish[4];
                }
                else
                {
                    return enemyFish[5];
                }
            default:
                break;
        }

        return enemyFish[Random.Range(0,stage+1)];
    }

}
