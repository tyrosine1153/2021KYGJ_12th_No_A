using UnityEngine;

public class HitAniManager : MonoBehaviour
{
    [Header("Animation")] [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private float alphaNum;

    private void Update()
    {
        if (GameManager.Instance.curDamageTime > 0)
            sprite.color = new Color(1, 1, 1, alphaNum);
        else
            sprite.color = new Color(1, 1, 1, 1);
    }
}