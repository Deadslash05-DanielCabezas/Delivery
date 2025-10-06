using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;

    private InputAction moveAction;

    private Vector2 moveDirection;
    private Vector2 oldDirection = Vector2.zero;
    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {

        if (moveAction.inProgress)
            playerMovement.accelerate(); 
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        playerMovement.jump();
    }
}
