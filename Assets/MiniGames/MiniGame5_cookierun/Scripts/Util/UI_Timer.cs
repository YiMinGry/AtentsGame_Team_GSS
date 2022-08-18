using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    Text timerTxt;
    public int timerCount = 15;
    int timer = 0;
    bool isTimerDone = false;
    public bool IsTimerDone {
        get => isTimerDone;
        private set
        {
            if (isTimerDone != value)
            {
                isTimerDone = value;
                if (isTimerDone)
                {
                    OnTimerDone?.Invoke();
                }
            }
        }
    }
    public System.Action OnTimerDone;

    private void Awake()
    {
        timerTxt = GetComponent<Text>();
        timer = timerCount;
    }

    private void Start()
    {
        StartCoroutine(CoTimeCount());
    }

    IEnumerator CoTimeCount()
    {
        yield return new WaitForSeconds(1.0f);
        timer--;
        timerTxt.text = $"({timer})";
    }

    public void ResetTimer()
    {
        timer = timerCount;
    }
}
