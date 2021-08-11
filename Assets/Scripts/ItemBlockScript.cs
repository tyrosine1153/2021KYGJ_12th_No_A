using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 이동 (명세에 없음), 효과, 생성 조건
public class ItemBlockScript : MonoBehaviour
{
    private static GameObject ItemPrefab;
    [SerializeField] private Item itemInfo;
    [SerializeField] private bool hasItem;


    void Start()
    {
        if (ItemPrefab == null)
        {
            ItemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        }

        hasItem = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            Vector3 pointOfImpact = contact.point;
            print(pointOfImpact);
            if ( collision.transform.CompareTag("Player") && (transform.position - pointOfImpact).y <= 0.5 && hasItem)
            {
                CreateItemObj();
                hasItem = false;
            }
        }
    }

    private void CreateItemObj()
    {
        // 아이탬 생성 방식따라 수정해야 함
        var item = Instantiate(ItemPrefab, transform);
        var itemScript = item.GetComponent<ItemScript>();

        itemScript.ItemInfo = itemInfo;

        // 대충 효과
    }
}
