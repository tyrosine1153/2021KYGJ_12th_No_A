using UnityEngine;

public class FrontParallax : MonoBehaviour
{
    [SerializeField] private Transform image;
    [SerializeField] private float speed;
    [SerializeField] private float smoothing = 1f;

    private Transform cam;
    private Vector3 camPos;
    private float parallaxScales;

    private void Start()
    {
        cam = Camera.main.transform;
        camPos = cam.position;
        parallaxScales = image.position.z * -1;
    }

    private void Update()
    {
        var parallax = (camPos.x - cam.position.x) * speed;
        var targetPosX = image.position.x + parallax;
        var TargetPos = new Vector3(targetPosX, image.position.y, image.position.z);
        image.position = Vector3.Lerp(image.position, TargetPos, smoothing + Time.deltaTime);
        camPos = cam.position;
    }
}