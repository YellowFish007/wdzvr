using System;
using UnityEngine;
namespace Engine
{
    public abstract class Singleton<T> where T : new()
    {
        private static T instance;
        protected Singleton()
        {

        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }

    public class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name);
                        _instance = singleton.AddComponent<T>();

                        DontDestroyOnLoad(singleton);

                    }
                    return _instance;
                }
            }
        }
    }
}