using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_SetBonusTimeText : MonoBehaviour
{
    public string bonusText = "";
    TextMesh textMesh;

    int index;
    public int Index => index;
    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = bonusText;

        if (bonusText == "B")
            index = 0;
        else if (bonusText == "O")
            index = 1;
        else if (bonusText == "N")
            index = 2;
        else if (bonusText == "U")
            index = 3;
        else if (bonusText == "S")
            index = 4;
        else if (bonusText == "T")
            index = 5;
        else if (bonusText == "I")
            index = 6;
        else if (bonusText == "M")
            index = 7;
        else if (bonusText == "E")
            index = 8;
    }
}
