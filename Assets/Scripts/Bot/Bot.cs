using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private TargetTracker _tracker;
    [SerializeField] private BotMover _mover;
    [SerializeField] private StepDetector _stepDetector;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Transform _groundCheckPoint;

    private void FixedUpdate()
    {
        if (_tracker.TryGetPursuitDirection(transform.position, out Vector3 pursuitDirection))
        {
            Vector3 normal = _groundDetector.GetNormal(_groundCheckPoint.position);
            bool canClimb = _stepDetector.CanClimbStep(transform.forward);

            _mover.Move(pursuitDirection, normal, canClimb, Time.fixedDeltaTime);
        }
    }
}