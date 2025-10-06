using UnityEngine;

public class GrindMovement : MonoBehaviour
{
    private MovementData data;

    private void Start()
    {
        data = GetComponent<MovementData>();
    }

    public void HandleMovement(Vector2 moveInput)
    {
        // TODO: Implement rail movement
    }

    public void HandleJump()
    {
        // TODO: Jump off rail
    }
    public void Accelerate() { /* handle speed differently */ }
}
