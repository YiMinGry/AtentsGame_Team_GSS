#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenshotUtill : EditorWindow
{
    [MenuItem("Window/ScreenshotUtill")] // �޴� ���
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
        if (GUILayout.Button("��ũ���� ���"))
        {
            CaptureScreenForPC("����");
        }
    }

    private void CaptureScreenForPC(string fileName)
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        try
        {
            ScreenCapture.CaptureScreenshot($"Assets/Screenshot/{fileName}_{timestamp}" + ".png");
            Debug.Log("���� ����");

        }
        catch
        {

            Debug.Log("�������");

        }
    }
}
#endif