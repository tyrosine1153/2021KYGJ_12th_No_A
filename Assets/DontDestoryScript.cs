using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryScript : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
