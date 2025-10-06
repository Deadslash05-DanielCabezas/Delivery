using UnityEngine;

[RequireComponent(typeof(MovementState))]
public class ColliderDetecter : MonoBehaviour
{
    private MovementState movementState;

    private void Start()
    {
        movementState = GetComponent<MovementState>();
        if (movementState == null)
            Debug.LogError("ColliderDetecter: Missing MovementState component on this GameObject!");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        EvaluateTag(hit.gameObject.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Working");
        EvaluateTag(other.gameObject.tag);
    }

    private void EvaluateTag(string tag)
    {
        switch (tag)
        {
            case "Wall":
                movementState.ChangeState(MovementState.moveState.wallrun);
                Debug.Log("State changed to: Wallrun");
                break;

            case "Rail":
                movementState.ChangeState(MovementState.moveState.grind);
                Debug.Log("State changed to: Grind");
                break;

            case "Floor":
                movementState.ChangeState(MovementState.moveState.normal);
                Debug.Log("State changed to: Normal");
                break;

            default:
                // Optional: you can leave this out if you don't want fallback logs
                Debug.Log($"Unknown tag detected: {tag}");
                break;
        }
    }
}
