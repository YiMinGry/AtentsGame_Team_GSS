using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    float heigthMin = -5.0f;
    float heigthMax = 5.0f;

    public GameObject[] enemyFish = null; // 0 ~ 5 에 1 ~ 6 레벨 물고기 각각 들어감

    public float spawnInterval = 1.0f; // 물고기 스폰간격

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
            GameObject obj = Instantiate(enemyFish[Random.Range(0,MG2_GameManager.Inst.Stage + 1)]); // 1 ~ 스테이지 레벨 까지 물고기 스폰
            obj.transform.position = this.transform.position; // 스포너 위치에 스폰
            obj.transform.Translate(Vector3.up * Random.Range(heigthMin, heigthMax)); // 스포너 위치에서 상하 랜덤하게 위치 조정
            if (this.transform.position.x < 0) // 왼쪽 스포너에서는 물고기가 반대 방향으로 가도록 조정
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z * (-1.0f));
            }
        }
    }

}
