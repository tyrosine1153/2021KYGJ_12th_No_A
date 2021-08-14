using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int maxHP = 5;
    public int curHP;

    // 피격 후 일정 시간동안 피격 무효
    public float maxDamageTime;
    public float curDamageTime;
    public bool isInSafeZone;

    public Item[] clearItems;
    public Dictionary<Item, bool> CurItem;

    private void Start()
    {
        curHP = maxHP;
        curDamageTime = maxDamageTime;
        isInSafeZone = false;

        Invoke(nameof(InitItem), 0.1f);
    }

    private void FixedUpdate()
    {
        if (curDamageTime > 0) curDamageTime -= Time.deltaTime;
    }

    private void InitItem()
    {
        CurItem = new Dictionary<Item, bool>();
        foreach (var item in clearItems) CurItem[item] = false;
        ViewCurItem();
    }

    public void GetDamagedHP(int damagePoint = 1)
    {
        if (curHP > 0)
        {
            curHP -= damagePoint;
            curDamageTime = maxDamageTime;
        }

        InGameUiManager.Instance.PlayerHeartUpdate();
        if (curHP <= 0) Die();
        print($"Player Get Damaged! HP : {curHP}/{maxHP}");
    }

    public void GetHealedHP(int healPoint = 1)
    {
        if (curHP < maxHP)
        {
            curHP += healPoint;
        }

        print($"Player Get Healed! HP : {curHP}/{maxHP}");
    }

    public void Die()
    {
        curHP = 0;
        print($"Player is Dead! HP : {curHP}/{maxHP}");

        InGameUiManager.Instance.ShowGameOver();
        PlayerScript.Instance.canControl = false;
        Invoke(nameof(GameOver), 3f);
    }

    public void GameOver()
    {
        print("Game Over!");
        InGameUiManager.Instance.RetryAskActive(true);
    }

    public void GetItem(Item item)
    {
        CurItem[item] = true;
        InGameUiManager.Instance.ItemSlotUpdate();

        print($"Player Get Item! {item}");
    }

    public bool IsClearableItem()
    {
        foreach (var item in clearItems)
            if (!CurItem[item])
                return false;

        return true;
    }

    public void StageClear()
    {
        if (StageManager.Instance.curStageNum == 8)
            StageManager.Instance.LoadStage(9);
        else
            StageManager.Instance.Fade(true);
    }

    [ContextMenu("정보")]
    private void ViewCurItem()
    {
        var itemText = new StringBuilder();
        itemText.Append("현재 아이템 목록\n");
        foreach (var item in CurItem) itemText.Append($"{item.Key} : {item.Value}\n");
        print(itemText.ToString());
    }
}