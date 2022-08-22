using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionHandle : MonoBehaviour
{
    public bool _isAttack = false;
    public bool _isRun = false;

    public void SetAttack(bool _TF) 
    {
        _isAttack = _TF;
    }
    public void SetRun(bool _TF)
    {
        _isRun = _TF;
    }
}
