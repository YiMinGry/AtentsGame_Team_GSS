using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAnyKey : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        StartCoroutine(TextBlink());
    }

    IEnumerator TextBlink()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (MG2_GameManager.Inst.Coin < 1)
            {
                text.text = "No Coin";
            }
            text.enabled = false;
            yield return new WaitForSeconds(0.5f);
            text.enabled = true;
        }
    }
}
