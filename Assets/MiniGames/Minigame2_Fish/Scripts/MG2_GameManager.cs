using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG2_GameManager : MonoBehaviour
{
    static MG2_GameManager instance;

    int score = 0;
    int stage = 1;
    int healthPoint = 4;

    public System.Action playerHPChange;

    public static MG2_GameManager Inst
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public int Score
    {
        get => score;
        set => score = value;
    }

    public int Stage
    {
        get => stage;
        set
        {
            stage = value;
            Debug.Log($"Stage : {stage}");
        }
    }

    public int HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = value;
            healthPoint = Mathf.Clamp(healthPoint, 0, 9);
            playerHPChange.Invoke();
            Debug.Log($"HealthPoint : {healthPoint}");
        }
    }

    public void Initialize()
    {
        score = 0;
        stage = 1;
        healthPoint = 4;
    }
}
