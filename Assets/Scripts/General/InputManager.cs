using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    private MovementManager movementManager;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("InputManager: No player assigned in inspector!");
            enabled = false;
            return;
        }

        movementManager = player.GetComponent<MovementManager>();

        if (movementManager == null)
        {
            Debug.LogError("InputManager: Player has no MovementManager component!");
            enabled = false;
            return;
        }

        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("No playerInput");
        }
    }

    private void Update()
    {
        movementManager.SetInputs(playerInput.actions["Move"].ReadValue<Vector2>(), playerInput.actions["Jump"].ReadValue<float>());
    }

    // --- Called automatically by PlayerInput via Unity Events ---
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // Debug check
        if (context.performed)
            Debug.Log($"[InputManager] Move Input: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("[InputManager] Jump Input Received");
            movementManager.Jump();
        }
    }
}
