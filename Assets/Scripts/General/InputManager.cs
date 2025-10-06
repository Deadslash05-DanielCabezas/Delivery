using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;

    private InputAction moveAction;

    private Vector2 moveDirection;
    private Vector2 oldDirection = new Vector2(0, 0);
    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {

        if (moveAction.inProgress)
            playerMovement.accelerate();

        if (moveAction.triggered)
        {
            moveDirection = moveAction.ReadValue<Vector2>();

            if (moveDirection.x != oldDirection.x)
            {
                oldDirection = moveDirection;
                if (moveDirection.x == -1)
                    playerMovement.turn(-90);
                else if (moveDirection.x == 1)
                    playerMovement.turn(90);
                else
                    playerMovement.stopTurning();
            }
        }

        if (moveAction.ReadValue<Vector2>() == Vector2.zero)
        {
            playerMovement.stopTurning();
        }    
    }
}
