using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MiniGame5_IUI
{
    uint Id { get; set; }
    void Init();
    void OpenScene();
    void CloseScene();
    void StartScene();
    void EndScene();
    void Refresh();
}

public interface MiniGame5_IScene
{
    uint Id { get; set; }
    void Init();
    void OpenScene();
    void CloseScene();
    void StartScene();
    void EndScene();
    void Refresh();
}

