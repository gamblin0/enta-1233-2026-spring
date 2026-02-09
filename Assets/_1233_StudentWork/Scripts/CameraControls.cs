using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public CinemachineCamera vcam1;
    public CinemachineCamera vcam2;

    private bool usingvcam1 = true;

    private void Awake()
    {
        // Auto-assign cameras if they were not set in Inspector
        if (vcam1 == null || vcam2 == null)
        {
            var cams = FindObjectsOfType<CinemachineCamera>();

            if (cams.Length < 2)
            {
                Debug.LogError("CameraControls: Not enough Cinemachine cameras found!");
                return;
            }

            vcam1 = cams[0];
            vcam2 = cams[1];
        }
    }

    public void OnCameraSwitch(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (vcam1 == null || vcam2 == null)
        {
            Debug.LogError("CameraControls: Camera references missing!");
            return;
        }

        Debug.Log("SwitchCamera PERFORMED");

        usingvcam1 = !usingvcam1;

        vcam1.Priority = usingvcam1 ? 10 : 5;
        vcam2.Priority = usingvcam1 ? 5 : 10;
    }
}

