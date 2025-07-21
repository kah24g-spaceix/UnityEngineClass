using UnityEngine;
public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
                if (m_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name + " (Singleton)");
                    m_instance = go.AddComponent<T>();
                }

                DontDestroyOnLoad(m_instance);
                return m_instance;
            }
            else return m_instance;
        }
    }
}