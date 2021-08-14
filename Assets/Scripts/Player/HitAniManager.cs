using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAniManager : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float alphaNum;

    void Update()
    {
        if (GameManager.Instance.curDamageTime > 0)
            sprite.color = new Color(1, 1, 1, alphaNum);
        else
            sprite.color = new Color(1, 1, 1, 1);
    }
}
