using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private bool loaded = false;//로딩이 완료 되었는지 체크하는 변수

    private void Awake()
    {
        Invoke("ToLobby", 1f);//함수의 이름으로 1초뒤에 해당 함수를 실행시키라는 기본 함수
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
