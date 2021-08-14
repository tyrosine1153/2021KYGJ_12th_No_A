using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPointScript : MonoBehaviour
{
    public Item[] clearItems;

    private void Start()
    {
        GameManager.Instance.clearItems = clearItems;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            TryStageClear();
        }
    }

    private void TryStageClear()
    {
        if (GameManager.Instance.IsClearableItem())
        {
            GameManager.Instance.StageClear();
        }
        else
        {
            // 대충 아이템이 부족하다는 효과
            print("Fail to Stage Clear!");
            TextBoxScript.Instance.TypeText("재료가 부족해.");
        }
    }
}
