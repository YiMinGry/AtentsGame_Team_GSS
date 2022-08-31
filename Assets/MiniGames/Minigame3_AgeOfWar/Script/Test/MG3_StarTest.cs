using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_StarTest : MonoBehaviour
{
    private void Start()
    {
        MG3_GameManager.Inst.Gold += 20000;
        //GameManager.Inst.Exp = 13500;
    }
}
