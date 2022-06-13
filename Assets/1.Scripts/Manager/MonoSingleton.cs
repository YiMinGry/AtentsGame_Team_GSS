using UnityEngine;
using System.Collections;

public abstract class MonoSingleton<T> : CachingMonoBehaviour where T : MonoSingleton<T>
{
    private static T m_Instance = null;

    public static T instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (m_Instance == null)
                {
                    GameObject pGameObject = new GameObject(typeof(T).ToString());
                    m_Instance = pGameObject.AddComponent<T>();
                }
            }

            return m_Instance;
        }
    }


    protected virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public virtual void Init()
    {

    }

    public virtual void Load() { this.enabled = true; }

    public virtual void UnLoad() { this.enabled = false; }

    protected override void OnDestroy()
    {
        m_Instance = null;

        StopAllCoroutines();
        CancelInvoke();

        base.OnDestroy();
    }

    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}