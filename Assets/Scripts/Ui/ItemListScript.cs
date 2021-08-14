using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListScript : MonoBehaviour
{
    [SerializeField] Image[] itemImages;
    private readonly Color existColor = new Color(1, 1, 1, 1);
    private readonly Color notExistColor = new Color(1, 1, 1, 0.5f);


    private void Start()
    {
        Invoke(nameof(ItemSlotInit), 0.2f);
    }

    public void ItemSlotInit()
    {
        var curItem = GameManager.Instance.curItem;
        foreach (var item in curItem)
        {
            itemImages[(int) item.Key].color = item.Value ? existColor : notExistColor;
        }
    }
}
