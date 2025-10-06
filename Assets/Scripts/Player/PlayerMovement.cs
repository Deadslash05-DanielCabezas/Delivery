using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform t;
    private CharacterController characterController;

    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 600;
    [SerializeField] private float acceleration = 4;
    [SerializeField] private float deceleration = 1;

    [Header("Turning Settings")]
    [SerializeField] private float baseTurnSpeed = 90f;          // Degrees per second
    [SerializeField] private float maxTurnIntensity = 1f;        // Max buildup value
    [SerializeField] private float turnBuildUpRate = 2f;         // How fast turn ramps up
    [SerializeField] private float turnDecayRate = 3f;           // How fast turn fades out
    [SerializeField] private float turnDampingFactor = 200f;     // How much speed reduces turning

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpPower = 50;
    [SerializeField] private float fallSpeed = -1;

    private Vector3 motion = Vector3.zero;
    private float verticalVelocity;
    private bool queueJump = false;

    private float turnInput;
    private float turnIntensity;
    private float previousTurnInput = 0f;

    private void Start()
    {
        t = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleRotation();          
        HandleForwardMotion();     
        HandleGravityAndJump();
        ApplyMotion();
        HandleDeceleration();
    }

    private void HandleRotation()
    {
        // Detect direction switch (ignoring near-zero input noise)
        bool switchedDirection =
            Mathf.Abs(turnInput) > 0.01f &&
            Mathf.Abs(previousTurnInput) > 0.01f &&
            Mathf.Sign(turnInput) != Mathf.Sign(previousTurnInput);

        if (switchedDirection)
            turnIntensity = 0f;

        // Smooth buildup or decay
        if (Mathf.Abs(turnInput) > 0.01f)
        {
            turnIntensity = Mathf.MoveTowards(
                turnIntensity,
                turnInput * maxTurnIntensity,
                turnBuildUpRate * Time.deltaTime
            );
        }
        else
        {
            turnIntensity = Mathf.MoveTowards(
                turnIntensity,
                0f,
                turnDecayRate * Time.deltaTime
            );
        }

        // Scale turn power inversely with speed
        float effectiveTurnSpeed = baseTurnSpeed * (1f / (1f + speed / turnDampingFactor));

        // Apply rotation
        t.Rotate(Vector3.up, turnIntensity * effectiveTurnSpeed * Time.deltaTime);

        // Store input for next frame
        previousTurnInput = turnInput;
    }


    private void HandleForwardMotion()
    {
        // Keep constant forward direction — rotation doesn’t add speed
        motion = t.forward * speed;
    }

    private void HandleGravityAndJump()
    {
        verticalVelocity += fallSpeed;

        if (queueJump)
        {
            verticalVelocity += jumpPower;
            queueJump = false;
        }

        if (characterController.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2;
    }

    private void ApplyMotion()
    {
        motion.y = verticalVelocity;
        characterController.Move(motion * Time.deltaTime);
    }

    private void HandleDeceleration()
    {
        speed -= deceleration;
        if (speed < 0)
            speed = 0;
    }

    public void Accelerate()
    {
        speed += acceleration;
        if (speed > maxSpeed)
            speed = maxSpeed;
    }

    public void Jump()
    {
        if (characterController.isGrounded)
            queueJump = true;
    }

    public void SetTurnInput(float input)
    {
        turnInput = Mathf.Clamp(input, -1f, 1f);
    }
}
