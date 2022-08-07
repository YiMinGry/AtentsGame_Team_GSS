using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : UnitSpawner
{
    GameStart gameStart;
    //private void Awake()
    //{
       
    //}

    override protected void Start()
    {
        isEnemy = true;
        base.Start();// units[,]이거 설정해줌
        //StartCoroutine(spawnEnemy());
        gameStart=FindObjectOfType<GameStart>();
        if(gameStart!=null)
            gameStart.OnClickStart += StartSpawnEnemy;
        
    }
    IEnumerator spawnEnemy()
    {
        while(true)
        {
            
            SpawnMeleeUnit();
            yield return new WaitForSeconds(5.0f);
        }
        
    }
    public void StartSpawnEnemy()
    {
        StartCoroutine(spawnEnemy());
    }
}
