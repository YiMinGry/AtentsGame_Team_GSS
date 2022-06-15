using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private bool loaded = false;

    private void Awake()
    {
        Invoke("ToLobby", 1f);
    }

    private void ToLobby()
    {
        if (!loaded)
        {
            bl_SceneLoaderManager.LoadScene("Main_Lobby");
            loaded = true;
        }
    }
}
