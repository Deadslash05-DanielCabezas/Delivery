using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private CinemachineCamera vCam;
    private Coroutine boostRoutine;
    private float originalFOV;
    private bool holdingZoom = false;

    [Header("Boost Settings")]
    public float zoomAmount = 5f;                  // How much to zoom in (smaller FOV)
    [Range(0f, 1f)] public float initialDecay = 0.2f; // How much of the zoom decays right after the burst
    public float zoomInSpeed = 10f;                // How fast to zoom in
    public float decaySpeed = 2f;                  // How fast the tiny decay happens
    public float releaseSpeed = 3f;                // How fast to return to normal after release

    void Awake()
    {
        vCam = GetComponent<CinemachineCamera>();
        if (vCam == null)
        {
            Debug.LogError("CameraEffects requires a CinemachineCamera component on the same GameObject!");
            enabled = false;
            return;
        }

        originalFOV = vCam.Lens.FieldOfView;
    }

    /// <summary>
    /// Starts the boost zoom effect and holds until ReleaseBoost() is called.
    /// </summary>
    public void TriggerBoost()
    {
        if (boostRoutine != null)
            StopCoroutine(boostRoutine);

        boostRoutine = StartCoroutine(DoBoost());
    }

    /// <summary>
    /// Ends the boost hold and returns camera to original zoom.
    /// </summary>
    public void ReleaseBoost()
    {
        if (boostRoutine != null)
            StopCoroutine(boostRoutine);

        boostRoutine = StartCoroutine(ReturnToNormal());
    }

    private IEnumerator DoBoost()
    {
        holdingZoom = true;
        float targetFOV = originalFOV - zoomAmount;
        float holdFOV = originalFOV - (zoomAmount * (1f - initialDecay));

        float t = 0f;

        // Step 1: Quick zoom-in burst
        while (t < 1f)
        {
            t += Time.deltaTime * zoomInSpeed;
            vCam.Lens.FieldOfView = Mathf.Lerp(originalFOV, targetFOV, t);
            yield return null;
        }

        // Step 2: Small decay back toward hold FOV
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * decaySpeed;
            vCam.Lens.FieldOfView = Mathf.Lerp(targetFOV, holdFOV, t);
            yield return null;
        }

        // Step 3: Hold this FOV until told otherwise
        vCam.Lens.FieldOfView = holdFOV;

        // Wait until release
        while (holdingZoom)
            yield return null;

        // Step 4: Return smoothly to normal
        t = 0f;
        float currentFOV = vCam.Lens.FieldOfView;
        while (t < 1f)
        {
            t += Time.deltaTime * releaseSpeed;
            vCam.Lens.FieldOfView = Mathf.Lerp(currentFOV, originalFOV, t);
            yield return null;
        }

        vCam.Lens.FieldOfView = originalFOV;
        boostRoutine = null;
    }

    private IEnumerator ReturnToNormal()
    {
        holdingZoom = false;
        yield break; // Let DoBoost handle the return transition
    }
}
