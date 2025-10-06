using UnityEngine;

public class WallrunMovement : MonoBehaviour
{
    private MovementData data;

    private void Start()
    {
        data = GetComponent<MovementData>();
    }

    public void HandleMovement(Vector2 moveInput)
    {
        // TODO: Implement wallrun movement
    }

    public void HandleJump()
    {
        // TODO: Jump off wall
    }
    public void Accelerate() { /* handle speed differently */ }
}
