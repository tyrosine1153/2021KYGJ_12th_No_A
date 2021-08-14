using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    [SerializeField] private GameObject ob;

    public void DestroySmoke()
    {
        Destroy(ob);
    }
}