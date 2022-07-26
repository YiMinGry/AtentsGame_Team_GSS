using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDetail : MonoBehaviour
{
    int id;
    Text textDetail;
    private void Awake()
    {
        textDetail=GetComponent<Text>();
    }

    public void expose(UnitData _unitdata)
    {
        textDetail.text = $"{_unitdata.UnitName} - ${_unitdata.cost}";
    }
    public void expose(TurretData turretData)
    {
        textDetail.text = $"{turretData.turretName} - ${turretData.cost}";
    }
    public void hide()
    {
        textDetail.text = "";
    }
   
}
