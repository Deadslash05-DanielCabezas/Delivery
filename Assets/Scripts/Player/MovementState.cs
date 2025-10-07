using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.XR;

public class MovementState : MonoBehaviour
{
    private GrindMovement grind;
    private void Start()
    {
        grind = GetComponent<GrindMovement>();
    }
    public enum moveState
    {
        normal,
        grind,
        wallrun,
    }

    public moveState state = moveState.normal;

    private moveState oldState = moveState.normal;


    public void ChangeState(moveState newState)
    {
        oldState = state;
        state = newState;

        switch(state)
        {
            case moveState.normal:
                if (oldState == moveState.grind)
                    grind.EndRail(); 
                break;

            case moveState.grind:
                grind.StartRail(); 
                break;
        }
    }
}
