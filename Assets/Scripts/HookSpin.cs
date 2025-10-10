using UnityEngine;

public class HookSpin : MonoBehaviour
{


    public string targetTag = "Player";
    
    public LayerMask targetLayers;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {

            Debug.Log($"Trigger Entro por: {other.name}");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {

            Debug.Log($"Trigger salido por: {other.name}");

        }
    }
}
