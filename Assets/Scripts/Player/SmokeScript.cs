using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    [SerializeField] GameObject ob;
    public void DestroySmoke()
    {
        Destroy(ob);
    }
}
