using UnityEngine;

[DisallowMultipleComponent]
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool dontDestroyOnLoad;

    protected static volatile T _instance;
    protected static object _syncRoot = new System.Object();

    public static T Instance {
        get {
            Initialize();
            return _instance;
        }
    }

    public static bool IsInitialized => _instance != null;

    public static void Initialize()
    {
        if (_instance != null)
        {
            return;
        }
        lock (_syncRoot)
        {
            _instance = FindObjectOfType<T>();

            if(_instance == null)
            {
                var go = new GameObject(typeof(T).FullName);
                _instance = go.AddComponent<T>();
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError(GetType().Name + " Singleton class is already created.");
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    protected virtual void OnAwake() { }
}