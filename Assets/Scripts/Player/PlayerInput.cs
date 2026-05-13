using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    public Vector3 GetDirection()
    {
        float xAxis = Input.GetAxis(HorizontalAxis);
        float zAxis = Input.GetAxis(VerticalAxis);

        return new Vector3(xAxis, 0, zAxis);
    }
}