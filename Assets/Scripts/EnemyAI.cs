using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _rigidbody;

    private float _nextPointDistance = 3f;
    private int _currentPointIndex = 0;

    public Vector3 CurrentPoint => _path.vectorPath[_currentPointIndex];
    public bool HasPath => _path != null && _currentPointIndex < _path.vectorPath.Count;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
    }

    public void BuildPath(Transform target)
    {
        if (_seeker != null && _seeker.IsDone())
            _seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    public void AdvanceIfReached()
    {
        if (!HasPath)
            return;

        if ((transform.position - CurrentPoint).sqrMagnitude <= _nextPointDistance * _nextPointDistance)
            _currentPointIndex++;
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _path = path;
            _currentPointIndex = 1;
        }
    }
}
