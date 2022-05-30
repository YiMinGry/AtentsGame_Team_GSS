using UnityEngine;

public class Singleton<T> where T : class, new()
{
    public static readonly T instance = new T();

    public Singleton()
    {
        Init();
    }

    public virtual void Init() { }
}

public class NormalSingleton<T> where T : class, new()
{
    private static T m_Instance;
    public static T instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new T();
            }

            return m_Instance;
        }
    }

    public virtual void Init() { }

    public virtual void UnLoad() { }

    public static void SetInstance(T instance)
    {
        m_Instance = instance;
    }
}
