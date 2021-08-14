using System;
using UnityEngine;

public class StartZoneScript : MonoBehaviour
{
    public Action<Collider2D> CollisionEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) CollisionEvent(other);
    }
}