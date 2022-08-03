using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UnitDataManager unitDataMgr;
    TurretDataManager turretDataMgr;
    private int gold = 200;
    private int exp = 0;
    private int revolution = 0;
    private int numTurretSlot=0;
    private int[] addSlotCosts = new int[] { 3000, 8000 };
    Menu menu;



    //ÇÁ·ÎÆÛÆ¼ -----------------------------------------------------
    public int Gold { get { return gold; } set { gold = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public int Revolution { get => revolution; }
    public int NumTurretSlot => numTurretSlot;
    public UnitDataManager UnitDataMgr => unitDataMgr;
    public TurretDataManager TurretDataMgr => turretDataMgr;
    public Menu Menu => menu;
    public int AddSlotCost => addSlotCosts[Math.Min(1, numTurretSlot)];
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

    }
    public void Revol()
    {
        if (Exp > 10000)
            revolution = 3;
        else if (Exp > 4000)
            revolution = 2;
        else if(Exp > 1)
            revolution = 1;
    }
    public void AddTurretSlot()
    {if(numTurretSlot<2&&gold>=AddSlotCost)
        {
            gold -= AddSlotCost;
            
            numTurretSlot++;
            
            
        }

        
    }
}
