using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    float heigthMin = -5.0f;
    float heigthMax = 5.0f;

    public GameObject[] enemyFish = null;

    public float spawnInterval = 1.0f;

    WaitForSeconds waitSecond;

    public int stage = 5;

    private void Start()
    {
        waitSecond = new WaitForSeconds(spawnInterval);
        StartCoroutine(Spawn()); 
    }

    IEnumerator Spawn()
    {
        while (true) 
        {
            yield return waitSecond; 
            GameObject obj = Instantiate(enemyFish[Random.Range(0,stage+1)]);
            obj.transform.position = this.transform.position;
            obj.transform.Translate(Vector3.up * Random.Range(heigthMin, heigthMax));
            if (this.transform.position.x < 0)
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z * (-1.0f));
            }
        }
    }
    private void Update()
    {
        //coolTimeCheck -= Time.deltaTime;
        //if (coolTimeCheck < 0)
        //{
        //    GameObject obj = new GameObject();
        //    obj = enemyFish[0];
        //    coolTimeCheck = coolTime;
        //    Instantiate(obj, transform);

        //}
    }


}
