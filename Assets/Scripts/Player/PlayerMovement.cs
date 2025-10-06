using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    private Rigidbody rb;
    private Transform t;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 600;
    [SerializeField] private float acceleration = 4;
    [SerializeField] private float deceleration = 1;
    [SerializeField] private float turnSpeed = 5;

    private float targetRotation;
    private float currentRotation;
    private float newRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
    }

    private void Update()
    {
        currentRotation = t.rotation.y;
        if (!Mathf.Approximately(currentRotation, targetRotation))
        {
            print("a");
            newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, turnSpeed);
            t.rotation = Quaternion.Euler(0, newRotation, 0);
        }

        //Update velocity
        rb.linearVelocity = speed * t.forward;
        speed -= deceleration;
        if (speed < 0)
            speed = 0;
    }

    public void accelerate()
    {
        speed += acceleration;
        if (speed >  maxSpeed)
            speed = maxSpeed;
    }

    public void turn(int angle)
    {
        print("c");
        targetRotation = t.rotation.y + angle;
    }

    public void stopTurning()
    {
        print("d");
        targetRotation = t.rotation.y;
    }
}
