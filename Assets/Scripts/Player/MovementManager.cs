using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private MovementState movementState;
    private MovementData movementData;

    private NormalMovement normalMovement;
    private GrindMovement grindMovement;
    private WallrunMovement wallrunMovement;

    private Vector2 moveInput;

    private void Start()
    {
        movementState = GetComponent<MovementState>();
        movementData = GetComponent<MovementData>();

        normalMovement = GetComponent<NormalMovement>();
        grindMovement = GetComponent<GrindMovement>();
        wallrunMovement = GetComponent<WallrunMovement>();
    }

    private void Update()
    {
        // Each frame, send stored input to the correct movement system
        HandleMovement(moveInput);
    }

    public void HandleMovement(Vector2 input)
    {
        switch (movementState.state)
        {
            case MovementState.moveState.normal:
                normalMovement.HandleMovement(input);
                break;

            case MovementState.moveState.grind:
                grindMovement.HandleMovement(input);
                break;

            case MovementState.moveState.wallrun:
                wallrunMovement.HandleMovement(input);
                break;
        }
    }

    public void Jump()
    {
        switch (movementState.state)
        {
            case MovementState.moveState.normal:
                normalMovement.HandleJump();
                break;

            case MovementState.moveState.grind:
                grindMovement.HandleJump();
                break;

            case MovementState.moveState.wallrun:
                wallrunMovement.HandleJump();
                break;
        }
    }

    public void Accelerate()
    {
        switch (movementState.state)
        {
            case MovementState.moveState.normal:
                normalMovement.Accelerate();
                break;

            case MovementState.moveState.grind:
                grindMovement.Accelerate();
                break;

            case MovementState.moveState.wallrun:
                wallrunMovement.Accelerate();
                break;
        }
    }

    // Called externally by InputManager
    public void SetInputs(Vector2 input, float jump)
    {
        moveInput = input;
        if(jump > 0) { Jump(); }

    }
}
