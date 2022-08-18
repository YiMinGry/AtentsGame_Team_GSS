using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UnitDataManager unitDataMgr;
    TurretDataManager turretDataMgr;
    private int gold = 200;
    [SerializeField]private int exp = 0;
    private int revolution = 0;
    private int numTurretSlot = 0;
    private int[] addSlotCosts = new int[] { 3000, 8000 };
    private int[] revolExps = new int[] { 4000, 10000 };
    private GameObject[] bases=new GameObject[3];
    public GameObject[] enemyBases;
    public GameObject[] enemyTurretSlot;
    Menu menu;
    private int enemyRevol = 0;



    //ÇÁ·ÎÆÛÆ¼ -----------------------------------------------------
    public int Gold { get { return gold; } set { gold = value; } }
    public int Exp 
    { 
        get { return exp; } 
        set 
        {
            exp = value;
            if (enemyRevol < 2 && GameManager.Inst.Exp > GameManager.Inst.RevolExps[enemyRevol] * 0.9f)
            {
                enemyBases[enemyRevol].SetActive(false);
                enemyRevol++;
                enemyBases[enemyRevol].SetActive(true);
                TurretSlot slot = enemyTurretSlot[enemyRevol].GetComponent<TurretSlot>();
                slot.SetTurret(enemyRevol);
            }
        } 
    }
    public int EnemyRevol { get => enemyRevol; }
    public int Revolution { get => revolution; }
    public int NumTurretSlot => numTurretSlot;
    public UnitDataManager UnitDataMgr => unitDataMgr;
    public TurretDataManager TurretDataMgr => turretDataMgr;
    public Menu Menu => menu;
    public int AddSlotCost => addSlotCosts[Math.Min(1, numTurretSlot)];
    public int[] RevolExps => revolExps;
    //½Ì±ÛÅæ--------------------------------------------------------------
    static GameManager instance = null;
    public static GameManager Inst
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    
    //--------------------------------------------------------------------------
    private void Initialize()
    {
        
        menu=FindObjectOfType<Menu>();
        turretDataMgr=GetComponent<TurretDataManager>();
        unitDataMgr=GetComponent<UnitDataManager>();
        Base[] baseparents = FindObjectsOfType<Base>();
        GameObject baseparent;
        for (int i=0;i<baseparents.Length;i++)
        {
            if(baseparents[i].CompareTag("Unit"))
            {
                baseparent = baseparents[i].transform.Find("Base").gameObject;
                for(int j=0;j<bases.Length;j++)
                {
                    bases[j] = baseparent.transform.GetChild(j).gameObject;
                    if(j>0)
                    {
                        bases[j].gameObject.SetActive(false);
                    }
                }
            }
        }
        TurretSlot slot = enemyTurretSlot[0].GetComponent<TurretSlot>();
        slot.SetTurret(0);
    }
    public void Revol()
    {
        if(revolution<2)
        {
            if (exp >= revolExps[revolution])
            {
                bases[revolution].SetActive(false);
                revolution++;
                bases[revolution].SetActive(true);
            }
        }
        
    }
    
    public void AddTurretSlot()
    {if(numTurretSlot<2&&gold>=AddSlotCost)
        {
            gold -= AddSlotCost;
            
            numTurretSlot++;
        }
    }
}
