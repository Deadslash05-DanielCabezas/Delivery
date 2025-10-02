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
        if (moveAction.IsPressed())
            playerMovement.accelerate();
    }
}
