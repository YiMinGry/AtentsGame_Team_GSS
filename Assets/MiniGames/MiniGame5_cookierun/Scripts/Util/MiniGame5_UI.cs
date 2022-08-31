using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_UI : MonoBehaviour
{
    int id;
    public int Id { get => id; set { id = value; } }

    bool isWait = true;
    public bool IsWait { get => isWait; }

    float waitSeconds = 0.0f;
    public float WaitSeconds { get => waitSeconds; }

    public virtual void Init()
    {
        Debug.Log($"ui state = {(UIState)id}");
    }

    public virtual void OpenScene()
    {
        gameObject.SetActive(true);
    }
    public virtual void CloseScene()
    {
        gameObject.SetActive(false);
    }

    public virtual void StartScene()
    {

    }
    public virtual void EndScene()
    {

    }

    public virtual void Refresh(ChoiceState state)
    {

    }

    public virtual void ReSet()
    {

    }
}
