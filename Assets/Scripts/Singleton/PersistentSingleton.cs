using UnityEngine;

public class PersistentSingleton<T> : MonoSingleton<T> where T : MonoBehaviour
{
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
