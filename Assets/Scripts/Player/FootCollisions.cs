using UnityEngine;

public class FootCollisions : MonoBehaviour
{
    private GameObject player;
    private ColliderDetecter detecter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform.parent.gameObject;
        detecter = player.GetComponent<ColliderDetecter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Working " + other.name);
        detecter.EvaluateTag(other.tag, other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        detecter.EvaluateExitTag(other.tag);
    }
}
