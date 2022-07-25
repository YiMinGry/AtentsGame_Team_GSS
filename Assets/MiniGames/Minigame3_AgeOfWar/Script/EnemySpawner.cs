using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : UnitSpawner
{
    //private void Awake()
    //{
       
    //}

    override protected void Start()
    {
        isEnemy = true;
        base.Start();
        StartCoroutine(spawnEnemy());
        
    }
    IEnumerator spawnEnemy()
    {
        while(true)
        {
            
            SpawnMeleeUnit();
            yield return new WaitForSeconds(5.0f);
        }
        
    }
}
