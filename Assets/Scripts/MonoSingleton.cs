using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;

    public static T instance
    {
        get 
        { 
            if(m_instance == null)
                m_instance = FindObjectOfType<T>();
            return m_instance;
        }
    }


    void Awake()
    {
        if(m_instance != null)
        {
            Destroy(gameObject);
        }
    }


}
