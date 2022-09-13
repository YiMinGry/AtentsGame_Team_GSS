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

        switch (bonusText)
        {
            case "B":
                index = 0;
                textMesh.color = new(1, 0, 1, 1);
                break;
            case "O":
                index = 1;
                textMesh.color = new(1, 0, 0, 1);
                break;
            case "N":
                index = 2;
                textMesh.color = new(1, 0.5f, 0, 1);
                break;
            case "U":
                index = 3;
                textMesh.color = new(1, 1, 0, 1);
                break;
            case "S":
                index = 4;
                textMesh.color = new(0, 1, 0, 1);
                break;
            case "T":
                index = 5;
                textMesh.color = new(0, 1, 1, 1);
                break;
            case "I":
                index = 6;
                textMesh.color = new(0, 0, 1, 1);
                break;
            case "M":
                index = 7;
                textMesh.color = new(0.5f, 0, 1, 1);
                break;
            case "E":
                index = 8;
                textMesh.color = new(1, 0, 1, 1);
                break;
        } 
    }
}
