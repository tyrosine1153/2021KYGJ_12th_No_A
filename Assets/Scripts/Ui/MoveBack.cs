using UnityEngine;
using UnityEngine.UI;

public class MoveBack : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public Image target;
    private float _offset;

    private void Update()
    {
        _offset += Time.deltaTime * scrollSpeed;
        target.material.mainTextureOffset = new Vector2(_offset, 0);
    }
}