using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListScript : MonoBehaviour
{
    [SerializeField] Image[] itemImages;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);

        ItemSlotUpdate();
    }

    public void ItemSlotUpdate()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            if (GameManager.Instance.curItem[(Item)i])
                itemImages[i].color = new Color(1, 1, 1, 0.5f);
            else
                itemImages[i].color = new Color(1, 1, 1, 1);
        }
    }
}
