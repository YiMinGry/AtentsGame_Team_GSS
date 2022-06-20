using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    protected GameObject mainCanvas;

    protected GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public virtual void MapLoad(string _path)
    {

    }

    public virtual void AddMainCanvas(string _uiName)
    {

    }
    public virtual void AddObject(string _name)
    {

    }
}
