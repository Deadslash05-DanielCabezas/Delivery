using UnityEngine;

[System.Serializable]
public class MovementData : MonoBehaviour
{
    [Header("Speed Settings")]
    public float speed = 0f;
    public float maxSpeed = 600f;
    public float acceleration = 4f;
    public float deceleration = 1f;

    [Header("Turn Settings")]
    public float baseTurnSpeed = 90f;
    public float maxTurnIntensity = 1f;
    public float turnBuildUpRate = 2f;
    public float turnDecayRate = 3f;
    public float turnDampingFactor = 200f;

    [Header("Jump & Gravity")]
    public float jumpPower = 50f;
    public float fallSpeed = -1f;

    [Header("Boosting")]
    public float boostCooldown = 5;
    public float boostDuration = 2;
    public float boostAccelerationMultiplier = 1.25f;
    public float boostMaxSpeedMultiplier = 1.5f;

    [Header("Rail Grinding")]
    public float railSpeedMultiplier = 1.1f;

    [Header("Wall Jumping")]
    public float wallFallSpeed = -0.2f;
    public float wallJumpHorizontalPower = 50;
    public float wallTimeUntilFall = 1.2f;
    [HideInInspector] public float maxWallAngleDifference = 10; //Ignorad por ahora

    [HideInInspector] public Vector3 motion;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool queueJump = false;
    [HideInInspector] public bool canBoost = true;

    [HideInInspector] public float wallFallTimer;
}
