using UnityEngine;

public class FrameLimiter : MonoBehaviour
{
    public int target = 60;
    void Awake()
    {

        Application.targetFrameRate = target;
    }
}
