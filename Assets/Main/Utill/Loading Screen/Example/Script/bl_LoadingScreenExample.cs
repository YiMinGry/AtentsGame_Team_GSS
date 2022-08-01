using Lovatto.SceneLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scene로딩할때 테스트용
public class bl_LoadingScreenExample : MonoBehaviour
{
    public string SceneName = "LoadExample";

    private bool loaded = false;

    public void ToLobby()
    {
        if (!loaded)
        {
            bl_SceneLoaderManager.LoadScene(SceneName);
            loaded = true;

        }
    }

    public void LoadScene() //스페이스바
    {
        if (!loaded)
        {
            bl_SceneLoaderManager.LoadScene(SceneName);
            loaded = true;
        }
    }
}