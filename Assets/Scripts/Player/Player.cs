using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Transform _groundCheckPoint;

    private void Update()
    {
        Vector3 inputDirection = _input.GetDirection();
        bool isGrounded = _groundDetector.CheckIsGrounded(_groundCheckPoint.position);
        Vector3 normal = _groundDetector.GetNormal(_groundCheckPoint.position);

        _mover.Move(inputDirection, transform.right, transform.forward, isGrounded, normal, Time.deltaTime);
    }
}