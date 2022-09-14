
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_EnemySpawner : MG3_UnitSpawner
{
    MG3_GameStart gameStart;
    float fRand;
    int unittype;
    bool spawnStart = false;
    
    float spawnCool;
    GameObject[] bases = new GameObject[3];
    int[] revolexp;
    
    

    override protected void Start()
    {
        isEnemy = true;
        base.Start();// units[,]이거 설정해줌

        //StartCoroutine(spawnEnemy());
        gameStart=FindObjectOfType<MG3_GameStart>();
        if(gameStart!=null)
            gameStart.OnClickStart += StartSpawnEnemy;
        spawnCool = Random.Range(1.0f, 3.0f);
        
    }

    private void StartSpawnEnemy()
    {
        spawnStart = true;
    }

    private void Update()
    {
        if (spawnStart&&!MG3_GameManager.Inst.IsGameover)
        {
            spawnCool -= Time.deltaTime;
            //if (enemyRevol < 2 && GameManager.Inst.Exp > GameManager.Inst.RevolExps[enemyRevol]*0.9f)
            //{
            //    //bases[enemyRevol].SetActive(false);
            //    enemyRevol++;
            //    //bases[enemyRevol].SetActive(true);
            //}
            if(spawnCool<0)
            {
                float expPercent = 0;
                if(MG3_GameManager.Inst.EnemyRevol==1)
                {
                    expPercent = (MG3_GameManager.Inst.Exp - MG3_GameManager.Inst.RevolExps[MG3_GameManager.Inst.EnemyRevol-1])
                    / (MG3_GameManager.Inst.RevolExps[MG3_GameManager.Inst.EnemyRevol] - MG3_GameManager.Inst.RevolExps[MG3_GameManager.Inst.EnemyRevol-1]);
                }
                else if(MG3_GameManager.Inst.EnemyRevol==0)
                {
                    expPercent = (MG3_GameManager.Inst.Exp )/ (MG3_GameManager.Inst.RevolExps[MG3_GameManager.Inst.EnemyRevol ]);
                }
                else if(MG3_GameManager.Inst.EnemyRevol==2)
                {
                    expPercent = (MG3_GameManager.Inst.Exp - MG3_GameManager.Inst.RevolExps[MG3_GameManager.Inst.EnemyRevol - 1]) / 10000;
                }

                fRand = Random.Range(0.0f, 1.0f);
                if(fRand>0.4f+0.25f*expPercent)
                {
                    unittype = 0;
                }
                else if(fRand>0.05f+0.2*expPercent)
                {
                    unittype = 1;
                }
                else
                {
                    unittype = 2;
                }
                SpawnUnit(unittype + 3*MG3_GameManager.Inst.EnemyRevol);
                spawnCool = Random.Range(5.0f, 8.0f);

            }
        }

    }
}
