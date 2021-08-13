using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MoveBack : MonoBehaviour
{

    public float ScrollSpeed = 0.5f;
    float Offset;
    public Image target;

    void Update()
    {
        Offset += Time.deltaTime * ScrollSpeed;
        target.material.mainTextureOffset = new Vector2(Offset, 0);
    }
}

