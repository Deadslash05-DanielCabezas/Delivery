using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;

    private InputAction moveAction;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        HandleMovementInput();
        HandleTurnInput();
    }

    private void HandleMovementInput()
    {

        if (moveAction.ReadValue<Vector2>().y > 0) playerMovement.Accelerate();
    }

    private void HandleTurnInput()
    {
        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        playerMovement.SetTurnInput(moveDir.x);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        playerMovement.Jump();
    }
}
