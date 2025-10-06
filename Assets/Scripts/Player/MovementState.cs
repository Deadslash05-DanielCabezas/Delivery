using NUnit.Framework.Constraints;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.XR;

public class MovementState : MonoBehaviour
{
    public enum moveState
    {
        normal,
        grind,
        wallrun,
    }


    public moveState state = moveState.normal;


    public void ChangeState(moveState newState)
    {
        state = newState;
    }
    
}
