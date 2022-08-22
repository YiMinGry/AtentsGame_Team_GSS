using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnResevation : MonoBehaviour
{
    public UnitSpawner spawner;
    Queue<int> unitQueue=new Queue<int>();
    Queue<float> buildTimeQueue = new Queue<float>();
    float coolTime = 0;
    float buildTime = 0;
    Image buildImage;
    Image resevImage;
    bool isSpawning = false;
    private void Start()
    {
        //spawner=FindObjectOfType<UnitSpawner>();
        buildImage = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        resevImage = transform.GetChild(1).GetChild(1).GetComponent<Image>();
    }

    void ReserveSpawn(int _unitType)
    {
        unitQueue.Enqueue(_unitType);
        int unitNum = _unitType + 3 * GameManager.Inst.Revolution;
        buildTimeQueue.Enqueue(GameManager.Inst.UnitDataMgr[unitNum].buildTime);
        resevImage.fillAmount = unitQueue.Count * 0.25f;
        isSpawning = true;
    }

    public void SpawnMeleeUnit()
    {
        ReserveSpawn(0);
    }
    public void SpawnRangeUnit()
    {
        ReserveSpawn(1);
    }
    public void SpawnEliteUnit()
    {
        ReserveSpawn(2);
    }
    private void Update()
    {
        if(isSpawning)
        {
            coolTime -= Time.deltaTime;
            

            if (coolTime < 0.0f)
            {
                Debug.Log("ÄðÅ¸ÀÓ0");
                
                if (unitQueue.Count > buildTimeQueue.Count)
                {
                    spawner.SpawnUnit(unitQueue.Dequeue());
                    resevImage.fillAmount = unitQueue.Count * 0.25f;
                    if (buildTimeQueue.Count == 0)
                    {
                        isSpawning = false;
                        buildTime = 0;
                        coolTime = 0;
                    }
                }
                if(buildTimeQueue.Count > 0)
                {
                    
                    buildTime = buildTimeQueue.Dequeue();
                    coolTime = buildTime;
                }
                
            }
            buildImage.fillAmount =(buildTime - coolTime)/buildTime;
        }
        
    }

}
