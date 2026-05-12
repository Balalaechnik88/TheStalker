using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _fallVelocityLimiter = -2f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private float _groundRayLength = 0.5f;
    [SerializeField] private LayerMask _groundLayer;

    private CharacterController _character;
    private Vector3 _velocity;
    private Vector3 _groundNormal;

    private void Awake()
    {
        _character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 inputDirection = transform.right * xAxis + transform.forward * zAxis;

        bool isGrounded = CheckGround();

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = _fallVelocityLimiter;
        }

        Vector3 moveDirection = Vector3.ProjectOnPlane(inputDirection, _groundNormal).normalized;

        _character.Move(moveDirection * _speed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;
        _character.Move(_velocity * Time.deltaTime);
    }

    private bool CheckGround()
    {
        bool isHit = Physics.CheckSphere(_groundChecker.position, _groundCheckRadius, _groundLayer);

        if (Physics.Raycast(_groundChecker.position, Vector3.down, out RaycastHit hit, _groundRayLength, _groundLayer))
        {
            _groundNormal = hit.normal;
        }
        else
        {
            _groundNormal = Vector3.up;
        }

        return isHit;
    }
}