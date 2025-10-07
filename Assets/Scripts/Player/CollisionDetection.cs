using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    Transform t;
    private MovementData data;
    private MovementState mState;

    private Transform tDestination;
    private void Start()
    {
        t = GetComponent<Transform>();
        data = GetComponent<MovementData>();
        mState = GetComponent<MovementState>();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) //Rails layer
        {
            tDestination = other.transform;
            if (t.position.y > tDestination.position.y && data.verticalVelocity <= 0)
            {
                t.parent = tDestination;
                t.position = new Vector3(t.position.x, tDestination.position.y, 0);
                t.parent = null;

                mState.ChangeState(MovementState.moveState.grind);
            }
        }
    }
}
