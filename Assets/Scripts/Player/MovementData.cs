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

    [HideInInspector] public Vector3 motion;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool queueJump = false;
}
