using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _sphereRadius = 0.2f;
    [SerializeField] private float _rayLength = 0.5f;
    [SerializeField] private LayerMask _groundLayer;

    public bool CheckIsGrounded(Vector3 center)
    {
        return Physics.CheckSphere(center, _sphereRadius, _groundLayer);
    }

    public Vector3 GetNormal(Vector3 center)
    {
        if (Physics.Raycast(center, Vector3.down, out RaycastHit hit, _rayLength, _groundLayer))
        {
            return hit.normal;
        }

        return Vector3.up;
    }
}