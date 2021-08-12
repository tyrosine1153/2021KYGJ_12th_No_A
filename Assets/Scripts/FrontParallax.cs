using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontParallax : MonoBehaviour
{
    [SerializeField] Transform image;
    [SerializeField] float speed;
    float parallaxScales;
    [SerializeField] float smoothing = 1f;

    Transform cam;
    Vector3 camPos;

    private void Start()
    {
        cam = Camera.main.transform;
        camPos = cam.position;
        parallaxScales = image.position.z * -1;
    }
    private void Update()
    {
        float parallax = (camPos.x - cam.position.x) * speed;
        float targetPosX = image.position.x + parallax;
        Vector3 TargetPos = new Vector3(targetPosX, image.position.y, image.position.z);
        image.position = Vector3.Lerp(image.position, TargetPos, smoothing + Time.deltaTime);
        camPos = cam.position;
    }
}
