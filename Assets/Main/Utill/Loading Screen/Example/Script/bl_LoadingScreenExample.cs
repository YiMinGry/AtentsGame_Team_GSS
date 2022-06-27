using Lovatto.SceneLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bl_LoadingScreenExample : MonoBehaviour
{
    public string SceneName = "LoadExample";

    private bool loaded = false;

    private void Update()
    {
        if (!loaded && Input.GetKeyDown(KeyCode.Space))
        {
            bl_SceneLoaderManager.LoadScene(SceneName);
            loaded = true;
        }
    }

    public void ToLobby()
    {
        if (!loaded)
        {
            bl_SceneLoaderManager.LoadScene(SceneName);
            loaded = true;

        }
    }
}