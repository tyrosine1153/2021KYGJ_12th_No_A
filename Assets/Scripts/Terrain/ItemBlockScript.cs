using UnityEngine;

// 아이템 이동 (명세에 없음), 효과, 생성 조건
public class ItemBlockScript : MonoBehaviour
{
    private static GameObject[] ItemPrefabs;
    [SerializeField] private Item itemInfo;
    [SerializeField] private bool hasItem;


    private void Start()
    {
        if (ItemPrefabs == null)
        {
            ItemPrefabs = new GameObject[5];
            for (var i = 0; i < ItemPrefabs.Length; i++) ItemPrefabs[i] = Resources.Load<GameObject>($"{(Item) i}Item");
        }

        hasItem = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            Vector3 pointOfImpact = contact.point;
            print(pointOfImpact);
            if (collision.transform.CompareTag("Player") && (transform.position - pointOfImpact).y >= 0.4 && hasItem)
            {
                CreateItemObj();
                hasItem = false;
            }
        }
    }

    private void CreateItemObj()
    {
        // 아이탬 생성 방식따라 수정해야 함
        var item = Instantiate(ItemPrefabs[(int) itemInfo], transform);
        var itemScript = item.GetComponent<ItemScript>();

        itemScript.ItemInfo = itemInfo;

        // 대충 효과
    }
}