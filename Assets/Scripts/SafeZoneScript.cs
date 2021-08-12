using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        print($"Enter {collider.tag}");
        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.isInSafeZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        print($"Exit {collider.tag}");
        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.isInSafeZone = false;
        }
    }

}
