using System.Collections;
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
        //HandleSpin();
        Accelerate(moveInput.y);

        //print();
    }

    private void HandleRotation()
    {
        bool switchedDirection = (turnInput < previousTurnInput) || (turnInput > previousTurnInput);
        

        if (switchedDirection) turnIntensity = 0f;

        if (Mathf.Abs(turnInput) > 0.01f)
            turnIntensity = Mathf.MoveTowards(turnIntensity, turnInput * data.maxTurnIntensity, data.turnBuildUpRate * Time.deltaTime);
        else
            turnIntensity = Mathf.MoveTowards(turnIntensity, 0f, data.turnDecayRate * Time.deltaTime);

        float effectiveTurnSpeed = data.baseTurnSpeed * (1f / (1f + data.speed / data.turnDampingFactor));
        t.Rotate(Vector3.up, turnIntensity * effectiveTurnSpeed * Time.deltaTime);
        previousTurnInput = turnInput;
    }

    private void HandleSpin()
    {
        

        //transform.RotateAround(Target.position, Vector3.up,50*Time.deltaTime);
        //Cuando el personaje detecte que entrado en Pivot Area
        //Comprobación de si gha enviado un input por teclado
        //Calculos de la rotación del objecto manteniendo la velocidad de entrad
        //Comprobar la dirección de entrada del jugador
            //Inplementar un sistema de orbitas
        //Salir del bucle cuando el jugador le de al input por teclado para salir y salir con x velocidad
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

    public void Accelerate(float scaler)
    {
        data.speed += data.acceleration * scaler;
        if (data.speed > data.maxSpeed)
            data.speed = data.maxSpeed;
    }

    public IEnumerator HandleBoost()
    {
        if (data.canBoost)
        {
            data.acceleration *= data.boostAccelerationMultiplier;
            data.maxSpeed *= data.boostMaxSpeedMultiplier;
            data.canBoost = false;


            GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CameraEffects>().TriggerBoost();

            yield return new WaitForSeconds(data.boostDuration);

            data.acceleration /= data.boostAccelerationMultiplier;
            data.maxSpeed /= data.boostMaxSpeedMultiplier;

            GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CameraEffects>().ReleaseBoost();

            yield return new WaitForSeconds(data.boostCooldown);

            data.canBoost = true;
        }

        StopCoroutine(HandleBoost());
        yield return null;
    }
}
