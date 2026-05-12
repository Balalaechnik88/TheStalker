using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pursuer : MonoBehaviour
{
    [Header("Target & Movement")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _stopDistance = 3f;
    [SerializeField] private float _rotationSpeed = 10f; 

    [Header("Step Handling")]
    [SerializeField] private Transform _lowerStepRay;
    [SerializeField] private Transform _upperStepRay;
    [SerializeField] private float _stepRayLength = 0.5f;
    [SerializeField] private float _stepSmooth = 5f;

    [Header("Ground Check & Normals")]
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundRayLength = 0.5f;
    [SerializeField] private LayerMask _obstacleLayer;

    private Rigidbody _rigidbody;
    private Vector3 _groundNormal;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        Vector3 directionToTarget = _target.position - transform.position;
        directionToTarget.y = 0;

        float sqrDistance = directionToTarget.sqrMagnitude;
        float sqrStopDistance = _stopDistance * _stopDistance;

        if (sqrDistance > sqrStopDistance)
        {
            UpdateGroundNormal();
            HandleStep();

            Vector3 moveDirection = directionToTarget.normalized;

            Vector3 projectedDirection = Vector3.ProjectOnPlane(moveDirection, _groundNormal).normalized;

            Vector3 newPosition = _rigidbody.position + projectedDirection * _speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(newPosition);

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void UpdateGroundNormal()
    {
        if (Physics.Raycast(_groundChecker.position, Vector3.down, out RaycastHit hit, _groundRayLength, _obstacleLayer))
        {
            _groundNormal = hit.normal;
        }
        else
        {
            _groundNormal = Vector3.up;
        }
    }

    private void HandleStep()
    {
        if (Physics.Raycast(_lowerStepRay.position, transform.forward, out RaycastHit lowerHit, _stepRayLength, _obstacleLayer))
        {
            if (Physics.Raycast(_upperStepRay.position, transform.forward, out RaycastHit upperHit, _stepRayLength, _obstacleLayer) == false)
            {
                _rigidbody.MovePosition(_rigidbody.position + Vector3.up * (_stepSmooth * Time.fixedDeltaTime));
            }
        }
    }
}