using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(MovementState))]
public class ColliderDetecter : MonoBehaviour
{
    private MovementState movementState;
    Transform t;
    private MovementData data;

    //Rail variables
    private bool canRail;
    private Transform tDestination;
    private float dot;

    private void Start()
    {
        t = GetComponent<Transform>();
        data = GetComponent<MovementData>();
        movementState = GetComponent<MovementState>();

        if (movementState == null)
            Debug.LogError("ColliderDetecter: Missing MovementState component on this GameObject!");
    }

    /* private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        EvaluateTag(hit.gameObject.tag, hit.gameObject);
    } */

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Working " + other.name);
        EvaluateTag(other.tag, other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        EvaluateExitTag(other.tag);
    }

    public void EvaluateTag(string tag, GameObject collider)
    {
        switch (tag)
        {
            case "Wall":
                movementState.ChangeState(MovementState.moveState.wallrun);
                Debug.Log("State changed to: Wallrun");
                break;

            case "Rail":
                if (movementState.state != MovementState.moveState.grind)
                {
                    canRail = RailPositioning(collider);
                    if (canRail)
                    {
                        movementState.ChangeState(MovementState.moveState.grind);
                        Debug.Log("State changed to: Grind");
                    }
                }
                
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

    public void EvaluateExitTag(string tag)
    {
        switch (tag)
        {
            case "Rail":
                movementState.ChangeState(MovementState.moveState.normal);
                Debug.Log("State changed to: Normal");
                break;
        }
    }

    private bool RailPositioning(GameObject collider)
    {
        tDestination = collider.transform;

        //Check that player is above rail and falling
        if (t.position.y > tDestination.position.y && data.verticalVelocity <= 0)
        {
            //Snap position to rail
            t.parent = tDestination;
            t.position = new Vector3(0, tDestination.position.y, t.position.z);
            t.parent = null;

            //Rotate position to rail forwards or backwards depending on angle you got on
            dot = Vector3.Dot(t.forward, tDestination.forward);
            if (dot >= 0)
                t.forward = tDestination.forward;
            else
                t.forward = -tDestination.forward;
            
            return true;
        }
        else
            return false; 
    }
}
