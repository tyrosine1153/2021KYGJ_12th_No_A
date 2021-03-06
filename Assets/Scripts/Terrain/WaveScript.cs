using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isNotStarted;
    [SerializeField] private GameObject startZone;
    private StartZoneScript _startZoneScript;

    private void Start()
    {
        isNotStarted = true;

        _startZoneScript = startZone.GetComponent<StartZoneScript>();
        _startZoneScript.CollisionEvent += WaveStart;
    }

    private void Update()
    {
        if (isNotStarted) return;
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            print("Player touched at Wave!");

            GameManager.Instance.GetDamagedHP(3);
        }

        if (other.collider.CompareTag("Item"))
        {
            TextBoxScript.Instance.TypeText($"파도가 {other.collider.name}을 먹었어!");
            Destroy(other.gameObject);
        }
    }

    private void WaveStart(Collider2D other)
    {
        if (isNotStarted)
        {
            print("Wave Start!");
            isNotStarted = false;
        }
    }
}