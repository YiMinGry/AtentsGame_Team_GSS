using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private bool loaded = false;//�ε��� �Ϸ� �Ǿ����� üũ�ϴ� ����

    private void Awake()
    {
        Invoke("ToLobby", 1f);//�Լ��� �̸����� 1�ʵڿ� �ش� �Լ��� �����Ű��� �⺻ �Լ�
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
