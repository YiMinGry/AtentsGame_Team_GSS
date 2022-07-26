using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldExp : MonoBehaviour
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
        GoldText.text = $"{GameManager.Inst.Gold}";
        ExpText.text = $"{GameManager.Inst.Exp}";
    }
}
