using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            print("Player touched at Wave!");
            
            GameManager.Instance.Die();
        }

        if (other.collider.CompareTag("Item"))
        {
            TextBoxScript.Instance.TypeText($"대충 파도가 {other.collider.name}을 먹었다는 스크립트!");
            Destroy(other.gameObject);
        }
    }
}
