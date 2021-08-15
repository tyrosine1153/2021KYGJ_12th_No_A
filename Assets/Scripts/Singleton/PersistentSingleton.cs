using UnityEngine;

public class PersistentSingleton<T> : MonoSingleton<T> where T : MonoBehaviour
{ 
    public bool dontDestroyOnLoad;

    protected override void Awake()
    {
        base.Awake();
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        OnAwake();
    }
}