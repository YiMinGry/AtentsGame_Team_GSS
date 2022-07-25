using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UnitDataManager unitDataMgr;
    private int gold = 200;
    private int exp = 0;
    private int revolution = 0;

    public int Gold
    {get{ return gold;} set{gold = value;}}
    public int Exp { get { return exp; } set { exp = value; } }
    public int Revolution { get => revolution; }

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
    //---------------------------------
    public UnitDataManager UnitDataMgr => unitDataMgr;
    //--------------------------------------------------------------------------
    private void Initialize()
    {
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
}
