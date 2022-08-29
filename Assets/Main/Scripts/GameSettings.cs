using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public bool isFullScreen = true;
    public int resolutionNum = 0;

    public void SetScreenMode(int v)
    {
        if (v == 0) isFullScreen = true;
        else if (v == 1) isFullScreen = false;

        SetScreen();
    }

    public void SetResolution(int v)
    {
        resolutionNum = v;

        SetScreen();
    }

    void SetScreen()
    {
        switch (resolutionNum)
        {
            case 0:
                Screen.SetResolution(1920, 1080, isFullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, isFullScreen);
                break;
            case 2:
                Screen.SetResolution(960, 540, isFullScreen);
                break;
            case 3:
                Screen.SetResolution(640, 360, isFullScreen);
                break;
        }

        Debug.Log($"Change Screen {resolutionNum}");
    }
}
