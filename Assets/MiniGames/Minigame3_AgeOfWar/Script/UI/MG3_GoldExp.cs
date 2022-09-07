using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG3_GoldExp : MonoBehaviour
{
    public Text GoldText;
    public Text ExpText;

    private void Awake()
    {
        GoldText = transform.Find("GoldText").GetComponent<Text>();
        ExpText = transform.Find("ExpText").GetComponent<Text>();
    }

    private void Update()
    {
        GoldText.text = $"{MG3_GameManager.Inst.Gold}";
        ExpText.text = $"{MG3_GameManager.Inst.Exp}";
    }
}
