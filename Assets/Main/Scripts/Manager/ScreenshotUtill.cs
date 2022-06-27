#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenshotUtill : EditorWindow
{
    [MenuItem("Window/ScreenshotUtill")] // 메뉴 등록
    private static void Init()
    {
        ScreenshotUtill window = (ScreenshotUtill)GetWindow(typeof(ScreenshotUtill));
        window.Show();
        window.titleContent.text = "Assets/ScreenshotUtill";
        window.minSize = new Vector2(340f, 150f);
        window.maxSize = new Vector2(340f, 150f);
    }

    void OnGUI()
    {
        if (GUILayout.Button("스크린샷 찍기"))
        {
            CaptureScreenForPC("스샷");
        }
    }

    private void CaptureScreenForPC(string fileName)
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        try
        {
            ScreenCapture.CaptureScreenshot($"Assets/Screenshot/{fileName}_{timestamp}" + ".png");
            Debug.Log("저장 성공");

        }
        catch
        {

            Debug.Log("저장실패");

        }
    }
}
#endif