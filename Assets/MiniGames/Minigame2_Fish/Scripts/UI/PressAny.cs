using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAny : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        if (MG2_GameManager.Inst.Coin > 0)
        {
            text.text = "Continue?";
        }
        else
        {
            text.text = "Insert Coin";
        }
        StartCoroutine(TextBlink());
    }

    IEnumerator TextBlink()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            text.enabled = false;
            yield return new WaitForSeconds(0.5f);
            text.enabled = true;
        }
    }
}
