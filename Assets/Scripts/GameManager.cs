using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int curHP;
    
    // 피격 후 일정 시간동안 피격 무효
    public float maxDamageTime;
    public float curDamageTime;
    public bool isInSafeZone;
    
    public Item[] clearItems;
    private Dictionary<Item, bool> curItem;

    void Start()
    {
        curHP = maxHP;
        curDamageTime = maxDamageTime;
        isInSafeZone = false;

        Invoke(nameof(InitItem), 0.5f);
    }

    private void FixedUpdate()
    {
        if (curDamageTime > 0)
        {
            curDamageTime -= Time.deltaTime;
        }
    }
    
    private void InitItem()
    {
        curItem = new Dictionary<Item, bool>();
        foreach (var item in clearItems)
        {
            curItem[item] = false;
        }
        ViewCurItem();
    }
    
    public void GetDamagedHP(int damagePoint = 1)
    {
        if (curHP > 0)
        {
            curHP -= damagePoint;
            curDamageTime = maxDamageTime;
            // 대충 효과
        }
        if(curHP == 0)
        {
            // 대충 뒤지는 효과
        }
        print($"Player Get Damaged! HP : {curHP}/{maxHP}");
    }

    public void GetHealedHP(int healPoint = 1)
    {
        if (curHP < maxHP)
        {
            curHP += healPoint;
        }
        else
        {
            // 대충 다 찼다는 효과
        }

        print($"Player Get Healed! HP : {curHP}/{maxHP}");
    }

    public void GetItem(Item item)
    {
        curItem[item] = true;
        // 대충 효과

        print($"Player Get Item! {curItem.Keys}");
    }

    public bool IsClearableItem()
    {
        foreach (var item in clearItems)
        {
            if (!curItem[item])
            {
                return false;        
            }
        }

        return true;
    }

    public void StageClear()
    {
        print("Success to Stage Clear!");
    }

    [ContextMenu("정보")]
    void ViewCurItem()
    {
        StringBuilder itemText = new StringBuilder();
        itemText.Append("현재 아이템 목록\n");
        foreach (var item in curItem)
        {
            itemText.Append($"{item.Key} : {item.Value}\n");
        }
        print(itemText.ToString());
    }
}
