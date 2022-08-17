using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_Scene : MonoBehaviour
{
    int id;
    public int Id { get => id; set { id = value; } }

    bool isWait = true;
    public bool IsWait { get => isWait; }

    protected float waitSeconds = 0.0f;
    public float WaitSeconds { get => waitSeconds; }

    public virtual void Init()
    {
        Debug.Log($"scene state = {(SceneState)id}");
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

    public virtual void ReSet()
    {

    }

    public virtual void ChangeCharator(ChoiceState state)
    {

    }
}