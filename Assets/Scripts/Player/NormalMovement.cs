using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class NormalMovement : MonoBehaviour
{
    private CharacterController controller;
    private MovementData data;
    private Transform t;

    private float turnInput;
    private float turnIntensity;
    private float previousTurnInput;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        data = GetComponent<MovementData>();
        t = transform;
    }

    public void HandleMovement(Vector2 moveInput)
    {
        turnInput = moveInput.x;
        HandleRotation();
        HandleHorizontalMotion();
        HandleGravityAndJump();
        ApplyMotion();
        HandleDeceleration();

        if (moveInput.y > 0) { Accelerate(); }
    }

    private void HandleRotation()
    {
        bool switchedDirection =
            Mathf.Abs(turnInput) > 0.01f &&
            Mathf.Abs(previousTurnInput) > 0.01f &&
            Mathf.Sign(turnInput) != Mathf.Sign(previousTurnInput);

        if (switchedDirection) turnIntensity = 0f;

        if (Mathf.Abs(turnInput) > 0.01f)
            turnIntensity = Mathf.MoveTowards(turnIntensity, turnInput * data.maxTurnIntensity, data.turnBuildUpRate * Time.deltaTime);
        else
            turnIntensity = Mathf.MoveTowards(turnIntensity, 0f, data.turnDecayRate * Time.deltaTime);

        float effectiveTurnSpeed = data.baseTurnSpeed * (1f / (1f + data.speed / data.turnDampingFactor));
        t.Rotate(Vector3.up, turnIntensity * effectiveTurnSpeed * Time.deltaTime);
        previousTurnInput = turnInput;
    }

    private void HandleHorizontalMotion()
    {
        data.motion = t.forward * data.speed;
    }

    private void HandleGravityAndJump()
    {
        data.verticalVelocity += data.fallSpeed;
        if (data.queueJump)
        {
            data.verticalVelocity += data.jumpPower;
            data.queueJump = false;
        }
        if (controller.isGrounded && data.verticalVelocity < 0)
            data.verticalVelocity = -2;
    }

    private void ApplyMotion()
    {
        data.motion.y = data.verticalVelocity;
        controller.Move(data.motion * Time.deltaTime);
    }

    private void HandleDeceleration()
    {
        data.speed -= data.deceleration;
        if (data.speed < 0)
            data.speed = 0;
    }

    public void HandleJump()
    {
        if (controller.isGrounded)
            data.queueJump = true;
    }

    public void Accelerate()
    {
        data.speed += data.acceleration;
        if (data.speed > data.maxSpeed)
            data.speed = data.maxSpeed;
    }

}
