using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 이동 (명세에 없음), 효과
public class ItemScript : MonoBehaviour
{
    private Item _itemInfo;
    private bool _canGetItem;

    public Item ItemInfo
    {
        set => _itemInfo = value;
    }

    private void Start()
    {
        _canGetItem = false;
        Invoke(nameof(CanGetItem), 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 아이템 먹기
        if (collision.transform.CompareTag("Player") && _canGetItem)
        {
            GetItem();
            _canGetItem = false;
        }
    }

    private void CanGetItem()
    {
        _canGetItem = true;
    }
    
    private void Move(){ }
    
    private void GetItem()
    {
        // 아이템 개수가 구현 되었는지는 확정이 안돼서 사용안함
        GameManager.Instance.GetItem(_itemInfo);
        
        // Animation
        Destroy(gameObject);
    }
}