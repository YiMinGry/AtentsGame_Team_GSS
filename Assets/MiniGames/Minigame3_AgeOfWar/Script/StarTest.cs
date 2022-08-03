using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTest : MonoBehaviour
{
    private void Start()
    {
        GameManager.Inst.Gold += 20000;
    }
}
