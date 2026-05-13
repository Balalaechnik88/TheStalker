using UnityEngine;

public class StepDetector : MonoBehaviour
{
    [SerializeField] private Transform _lowerRay;
    [SerializeField] private Transform _upperRay;
    [SerializeField] private float _rayLength = 0.5f;
    [SerializeField] private LayerMask _obstacleLayer;

    public bool CanClimbStep(Vector3 forwardDirection)
    {
        bool hitLower = Physics.Raycast(_lowerRay.position, forwardDirection, out _, _rayLength, _obstacleLayer);
        bool hitUpper = Physics.Raycast(_upperRay.position, forwardDirection, out _, _rayLength, _obstacleLayer);

        return hitLower && !hitUpper;
    }
}