using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void Move(InputAction.CallbackContext dir)
    {
        Vector2 direction = dir.ReadValue<Vector2>();
        playerMovement.accelerate();
    }
}
