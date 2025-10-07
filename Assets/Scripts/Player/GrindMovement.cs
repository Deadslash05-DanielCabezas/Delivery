using System.Collections;
using UnityEngine;

public class GrindMovement : MonoBehaviour
{
    private CharacterController controller;
    private MovementData data;
    private Transform t;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        data = GetComponent<MovementData>();
        t = transform;
    }

    public IEnumerator RailUpdate()
    {
        for (; ; )
        {
            RailMovement();
            RailJump();
            GrindRail();
            CheckForEnd();
            yield return null;
        }
    }

    private void GrindRail()
    {
        data.motion.y = data.verticalVelocity;
        controller.Move(data.motion * Time.deltaTime);
    }

    private void RailJump()
    {
        if (data.queueJump)
        {
            data.verticalVelocity += data.jumpPower;
            data.queueJump = false;
        }
    }

    private void RailMovement()
    {
        data.motion = t.forward * data.speed;
    }

    private void CheckForEnd()
    {
        if (data.verticalVelocity > 0)
            EndRail();
    }

    public void HandleJump()
    {
        if (controller.isGrounded)
            data.queueJump = true;
    }
    public void StartRail()
    {
        data.speed *= data.railSpeedMultiplier;
        data.maxSpeed *= data.railSpeedMultiplier;
        data.verticalVelocity = -2;
        StartCoroutine( RailUpdate() );
    }

    public void EndRail()
    {
        data.speed /= data.railSpeedMultiplier;
        data.maxSpeed /= data.railSpeedMultiplier;
        StopCoroutine( RailUpdate() );
    }
}
