using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(MovementState))]
public class ColliderDetecter : MonoBehaviour
{
    private MovementState movementState;
    private Transform t;
    private MovementData data;
    private CharacterController controller;

    //Rail and Wall variables
    private Transform tCollider;
    private Vector3 snapPoint;
    private Vector3 projectedVector;
    private float dot;

    //Rail variables
    private bool canRail;
    private Transform tRail;
    private Vector3 railToPlayer;

    //Wall variables
    private bool canWall;
    private Collider wallCollider;
    private Transform tWall;


    private void Start()
    {
        t = GetComponent<Transform>();
        data = GetComponent<MovementData>();
        movementState = GetComponent<MovementState>();
        controller = GetComponent<CharacterController>();

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
                if (movementState.state != MovementState.moveState.wallrun)
                {
                    canWall = WallPositioning(collider);
                    if (canWall)
                    {
                        movementState.ChangeState(MovementState.moveState.wallrun);
                        Debug.Log("State changed to: Wall");
                    }
                }

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

            case "Wall":
                movementState.ChangeState(MovementState.moveState.normal);
                Debug.Log("State changed to: Normal");
                break;
        }
    }

    private bool RailPositioning(GameObject collider)
    {
        tCollider = collider.transform;

        //Check that player is above rail and falling
        if (t.position.y > tCollider.position.y && data.verticalVelocity <= 0)
        {
            //Snap position to rail
            tRail = tCollider.parent.transform;
            railToPlayer = t.position - tRail.position;
            projectedVector = Vector3.Project(railToPlayer, tRail.forward);
            snapPoint = projectedVector + tRail.position;
            snapPoint.y = tCollider.position.y;

            controller.Move(snapPoint - t.position);

            //Rotate position to rail forwards or backwards depending on angle you got on
            dot = Vector3.Dot(t.forward, tRail.forward);
            if (dot >= 0)
                t.forward = tRail.forward;
            else
                t.forward = -tRail.forward;
            
            return true;
        }
        else
            return false; 
    }

    private bool WallPositioning(GameObject collider)
    {
        tCollider = collider.transform;

        //Check that player is looking at wall
        if (Vector3.Angle(t.forward, -tCollider.right) < data.maxWallAngleDifference) //maxWallAngleDifference = 10 for now
        {
            //Snap to wall
            tWall = tCollider.parent.transform;
            wallCollider = tCollider.parent.GetComponent<Collider>();
            snapPoint = wallCollider.ClosestPoint(t.position);
            controller.Move(snapPoint - t.position);

            //Rotate position 90º of the wall
            t.forward = -tWall.right;

            return true;
        }
        else
            return false;
    }
}
