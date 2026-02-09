using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraControls : MonoBehaviour
{
    public CinemachineCamera vcam1;
    public CinemachineCamera vcam2;

    private bool usingVcam1 = true;

    // Subscribe when this object becomes active
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unsubscribe when disabled / destroyed
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called AFTER a scene (including additive) finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If cameras are already assigned, do nothing
        if (vcam1 != null && vcam2 != null)
            return;

        var cams = Object.FindObjectsByType<CinemachineCamera>(
            FindObjectsSortMode.None
        );

        if (cams.Length < 2)
        {
            Debug.LogError(
                "CameraControls: Not enough Cinemachine cameras found after scene load!"
            );
            return;
        }

        // Assign first two found cameras
        vcam1 = cams[0];
        vcam2 = cams[1];

        // Force a known starting state
        vcam1.Priority = 10;
        vcam2.Priority = 5;
        usingVcam1 = true;

        Debug.Log(
            $"CameraControls: Cameras assigned -> {vcam1.name}, {vcam2.name}"
        );
    }

    // Input callback
    public void OnCameraSwitch(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (vcam1 == null || vcam2 == null)
        {
            Debug.LogError("CameraControls: Camera references are missing!");
            return;
        }

        Debug.Log("SwitchCamera PERFORMED");

        usingVcam1 = !usingVcam1;

        vcam1.Priority = usingVcam1 ? 10 : 5;
        vcam2.Priority = usingVcam1 ? 5 : 10;
    }
}


