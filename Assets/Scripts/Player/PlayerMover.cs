using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _fallVelocityLimiter = -2f;

    private CharacterController _character;
    private Vector3 _verticalVelocity;

    private void Awake()
    {
        _character = GetComponent<CharacterController>();
    }

    public void Move(Vector3 inputDirection, Vector3 right, Vector3 forward, bool isGrounded, Vector3 groundNormal, float deltaTime)
    {
        Vector3 worldDirection = right * inputDirection.x + forward * inputDirection.z;
        Vector3 projectedDirection = Vector3.ProjectOnPlane(worldDirection, groundNormal).normalized;

        Vector3 horizontalMovement = projectedDirection * _speed;

        if (isGrounded && _verticalVelocity.y < 0)
        {
            _verticalVelocity.y = _fallVelocityLimiter;
        }

        _verticalVelocity.y += _gravity * deltaTime;

        Vector3 finalMovement = horizontalMovement + Vector3.up * _verticalVelocity.y;
        _character.Move(finalMovement * deltaTime);
    }
}