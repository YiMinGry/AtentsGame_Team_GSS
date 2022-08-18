using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Blinking : MonoBehaviour
{
    CanvasGroup canvas;

    public int speed = 3;
    float time = 0;

    public bool isBlinking = true;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        if (canvas == null)
        {
            gameObject.AddComponent<CanvasGroup>();
            canvas = GetComponent<CanvasGroup>();
        }
    }

    private void Update()
    {
        if (isBlinking)
        {
            float alpha = (Mathf.Cos(time) * 0.5f) + 0.5f;
            canvas.alpha = alpha;
            time += Time.deltaTime * (float)speed;
        }
    }
}
