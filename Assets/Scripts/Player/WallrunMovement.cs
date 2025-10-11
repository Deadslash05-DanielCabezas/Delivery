using UnityEngine;

public class WallrunMovement : MonoBehaviour
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

    public void HandleMovement()
    {
        WallMovement();
        WallJump();
        WallGravity();
        WallCalculations();
    }

    private void WallCalculations()
    {
        data.motion.y = data.verticalVelocity;
        controller.Move(data.motion * Time.deltaTime);
    }

    private void WallJump()
    {
        if (data.queueJump)
        {
            data.verticalVelocity += data.jumpPower;
            t.forward = -t.forward;
            data.motion = data.wallJumpHorizontalPower * t.forward;
            data.queueJump = false;
        }
    }

    private void WallGravity()
    {
        if (Time.time > data.wallFallTimer)
            data.verticalVelocity += data.wallFallSpeed;
    }

    private void WallMovement()
    {
        //data.motion = t.forward * data.speed;
    }

    public void HandleJump()
    {
        data.queueJump = true;
    }

    public void StartWall()
    {
        data.motion = Vector3.zero;
        data.verticalVelocity = 0;
        data.wallFallTimer = Time.time + data.wallTimeUntilFall;
    }

    public void EndWall()
    {
        //Just in case
    }

}
