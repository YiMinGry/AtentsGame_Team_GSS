using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ResizeTextUI : MonoBehaviour
{
    public RectTransform targetTrn;
    public Text targetText;
    HorizontalLayoutGroup layout;
    
    private void Awake()
    {
        if (targetTrn == null) targetTrn = GetComponent<RectTransform>();
        if (targetText == null) targetText = GetComponent<Text>();
        layout = GetComponent<HorizontalLayoutGroup>();
    }

    public void RefreshText()
    {
        if (targetText.preferredWidth < 250) {
            targetTrn.sizeDelta = new(targetText.preferredWidth + 10f, targetTrn.sizeDelta.y);
        }
        else
        {
            targetTrn.sizeDelta = new(250f, targetTrn.sizeDelta.y);
        }

        layout.SetLayoutHorizontal();
    }

    public void onChangeText()
    {
        targetTrn.sizeDelta = new(250f, 60f);
        layout.SetLayoutHorizontal();
    }
}
