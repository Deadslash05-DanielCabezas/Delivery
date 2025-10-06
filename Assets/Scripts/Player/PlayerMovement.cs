using Unity.Android.Gradle.Manifest;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    private Transform t;
    private CharacterController characterController;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 600;
    [SerializeField] private float acceleration = 4;
    [SerializeField] private float deceleration = 1;
    [SerializeField] private float turnSpeed = 5;
    [SerializeField] private float jumpPower = 50;
    [SerializeField] private float fallSpeed = -1;

    private Vector3 motion = Vector3.zero;
    private float verticalVelocity;

    private bool queueJump = false;

    private void Start()
    {
        t = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Set horizontal motion
        motion = t.forward * speed;

        //Set falling speed
        verticalVelocity += fallSpeed;

        //Jump if queued (maybe later add system to allow it to queue a few frames before touching ground)
        if (queueJump)
        {
            verticalVelocity += jumpPower;
            queueJump = false;
        }

        //Snap to ground
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2;
        }

        //Apply motion
        motion.y = verticalVelocity;
        characterController.Move(motion * Time.deltaTime);

        //Handle speed deceleration
        speed -= deceleration;
        if (speed < 0)
            speed = 0;
        
    }

    public void accelerate() //Accelerate speed (deceleration will still be calculated in Update)
    {
        speed += acceleration;
        if (speed >  maxSpeed)
            speed = maxSpeed;
    }

    public void jump() //Queue a jump action
    {
        if (characterController.isGrounded)
        {
            queueJump = true;
        }
            
    }
}
