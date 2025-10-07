using UnityEngine;
using UnityEngine.SceneManagement;
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
            case "Restart":
                GetComponent<CharacterController>().enabled = false;
                Debug.Log("Trying: " + GameObject.FindGameObjectWithTag("Respawn").gameObject.transform.position);
                transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
                GetComponent<CharacterController>().enabled = true;

                GameObject.FindGameObjectWithTag("GameController").GetComponent<Timer>().ResetTimer();

                var data = gameObject.GetComponent<MovementData>();
                data.speed = 0f;
                data.motion = Vector3.zero;
                data.verticalVelocity = 0f;
                data.queueJump = false;
                data.canBoost = true;
                break;

            case "StartTimer":
                GameObject.FindGameObjectWithTag("GameController").GetComponent<Timer>().StartTimer();
                break;
            case "EndTimer":
                GameObject.FindGameObjectWithTag("GameController").GetComponent<Timer>().FinishTimer();
                break;
            default:
                // Optional: you can leave this out if you don't want fallback logs
                Debug.Log($"Unknown tag detected: {tag}");
                break;
        }
    }
}
