using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _stopDistance = 3f;

    private float _sqrStopDistance;

    private void Awake()
    {
        _sqrStopDistance = _stopDistance * _stopDistance;
    }

    public bool TryGetPursuitDirection(Vector3 currentPosition, out Vector3 direction)
    {
        direction = Vector3.zero;

        if (_target == null) return false;

        Vector3 vectorToTarget = _target.position - currentPosition;
        vectorToTarget.y = 0;

        if (vectorToTarget.sqrMagnitude > _sqrStopDistance)
        {
            direction = vectorToTarget.normalized;
            return true;
        }

        return false;
    }
}