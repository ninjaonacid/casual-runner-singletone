using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    
    private static T instance;
    public static T Instance
    {
        get
        {
           
            
                if ((object)instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if ((object)instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = typeof(T).Name;
                        instance = go.AddComponent<T>();
                    }
                }
            
            return instance;
        }
    }
    

   

    private void OnDestroy()
    {
        Debug.Log("destroyed" + this.gameObject.name);
        
    }

    public virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        instance = this as T;
        DontDestroyOnLoad(this.gameObject);
    }
}
