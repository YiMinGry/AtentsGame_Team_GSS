using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager
{
    // 아무 스크립트에서나 ResourceManager.Inst.Instantiate 로 Resorces 폴더 안의 Prefabs 폴더에 있는 에셋파일을 찾아올 수 있음


    /// <summary>
    /// Resource 폴더를 시작 위치로 한 “path”에 해당하는 T 타입의 에셋 파일을 불러오고 이를 리턴한다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// Resource 폴더의 Prefab 폴더에서 찾아 온다. ($”Prefabs/{path}”)
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

    // 유일한 인스턴스
    private static ResourceManager inst;

    // 싱글톤
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