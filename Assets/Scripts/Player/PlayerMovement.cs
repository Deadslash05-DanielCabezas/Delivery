using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    private Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 600;
    [SerializeField] private float acceleration = 4;
    [SerializeField] private float deceleration = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Update velocity
        rb.linearVelocity = speed * Vector3.forward;
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
}
