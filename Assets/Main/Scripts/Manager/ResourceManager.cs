using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager
{
    // �ƹ� ��ũ��Ʈ������ ResourceManager.Inst.Instantiate �� Resorces ���� ���� Prefabs ������ �ִ� ���������� ã�ƿ� �� ����


    /// <summary>
    /// Resource ������ ���� ��ġ�� �� ��path���� �ش��ϴ� T Ÿ���� ���� ������ �ҷ����� �̸� �����Ѵ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// Resource ������ Prefab �������� ã�� �´�. ($��Prefabs/{path}��)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Filed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }

    // ������ �ν��Ͻ�
    private static ResourceManager inst;

    // �̱���
    public static ResourceManager Inst
    {
        get
        {
            if (inst == null)
            {
                inst = new ResourceManager();
            }

            return inst;
        }
    }

}