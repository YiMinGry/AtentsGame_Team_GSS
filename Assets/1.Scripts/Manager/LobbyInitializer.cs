using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyInitializer : SceneInitializer
{
    [SerializeField]
    private bool isDev = false;
    [SerializeField]
    private LobbyUI lobbyUI;

    void Start()
    {
        MapLoad("Prefabs/Lobby/3DMap" + (isDev == true ? "_Dev" : ""));

        AddMainCanvas("Achivement");
        AddMainCanvas("Ranking");
        AddMainCanvas("chat");
        AddMainCanvas("Gacha");

    }

    public override void MapLoad(string _path)
    {
        GameObject _map = Instantiate(Resources.Load<GameObject>(_path));
    }

    public override void AddMainCanvas(string _uiName)
    {
        if (mainCanvas == null)
        {
            mainCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/MainCanvas"));
        }

        GameObject _obj = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/" + _uiName), mainCanvas.transform);
        _obj.name = _uiName;

    }

    public override void AddObject(string _name)
    {
        GameObject _obj = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/" + _name));
        _obj.name = _name;
    }
}
