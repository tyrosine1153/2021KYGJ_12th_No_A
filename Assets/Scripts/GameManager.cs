using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int curHP;
    [SerializeField] private Dictionary<Item, int> curItem;

    void Start()
    {
        curHP = maxHP;
        curItem = new Dictionary<Item, int>();
    }

    public void GetDamagedHP(int damagePoint = 1)
    {
        if (curHP > 0)
        {
            curHP -= damagePoint;
            print($"Player Get Damaged! HP : {curHP}/{maxHP}");
            // 대충 효과
        }
        else
        {
            // 대충 뒤지는 효과
        }
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
    }

    public void GetItem(Item item, int num = 1)
    {
        curItem[item] += num;
        // 대충 효과
    }

    public bool IsClearableItem(Dictionary<Item, int> clearItems)
    {
        foreach (var item in clearItems.Keys)
        {
            if (curItem[item] == 0)
            {
                return false;        
            }
        }

        return true;
    }

    public void StageClear()
    {
        
    }

    [ContextMenu("정보")]
    void ViewCurItem()
    {
        
    }
}
