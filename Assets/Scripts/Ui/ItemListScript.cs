using UnityEngine;
using UnityEngine.UI;

public class ItemListScript : MonoBehaviour
{
    [SerializeField] private Image[] itemImages;
    private static readonly Color ExistColor = new Color(1, 1, 1, 1);
    private static readonly Color NotExistColor = new Color(1, 1, 1, 0.5f);


    private void Start()
    {
        Invoke(nameof(ItemSlotInit), 0.2f);
    }

    public void ItemSlotInit()
    {
        var curItem = GameManager.Instance.CurItem;
        foreach (var item in curItem) itemImages[(int) item.Key].color = item.Value ? ExistColor : NotExistColor;
    }
}