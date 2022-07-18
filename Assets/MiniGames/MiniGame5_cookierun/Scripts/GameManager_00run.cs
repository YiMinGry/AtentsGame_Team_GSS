using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_00run : MonoBehaviour
{
    int score = 0;
    int coin = 0;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            Debug.Log(score);
        }
    }

    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
        }
    }

    public Text scoreText;

    static GameManager_00run instance;
    public static GameManager_00run Inst { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Inintialize();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� ���ӸŴ����� ������ �������� ���
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Inintialize()
    {

    }
}
