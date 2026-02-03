using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public CinemachineCamera vcam1;
    public CinemachineCamera vcam2;

    private bool usingvcam1 = true;

    // Update is called once per frame
    public void OnCameraSwitch(InputAction.CallbackContext context)
    {
       

        if (!context.performed) return;
        
        Debug.Log("SwitchCamera PERFORMED");

        usingvcam1 = !usingvcam1;

        vcam1.Priority = usingvcam1 ? 10 : 5;
        vcam2.Priority = usingvcam1 ? 5 : 10;
            
        
    }
}
