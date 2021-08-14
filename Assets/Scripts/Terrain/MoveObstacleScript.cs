using UnityEngine;

public class MoveObstacleScript : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    private int _dest;

    private Vector3 _startPosition;
    private Vector3 _turnPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _turnPosition = _startPosition;
        _turnPosition.x += moveDistance;

        _dest = 1;
    }

    private void Update()
    {
        if (transform.position.x < _startPosition.x && _dest == -1)
            _dest = 1;
        else if (_turnPosition.x < transform.position.x && _dest == 1) _dest = -1;

        transform.Translate(Vector3.right * _dest * moveSpeed * Time.deltaTime);
    }
}