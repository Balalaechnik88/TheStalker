using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _stepLift = 5f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 moveDirection, Vector3 groundNormal, bool shouldClimb, float fixedDeltaTime)
    {
        Vector3 projectedDir = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;
        Vector3 newPosition = _rigidbody.position + projectedDir * (_speed * fixedDeltaTime);

        if (shouldClimb)
        {
            newPosition += Vector3.up * (_stepLift * fixedDeltaTime);
        }

        _rigidbody.MovePosition(newPosition);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed * fixedDeltaTime);
        }
    }
}