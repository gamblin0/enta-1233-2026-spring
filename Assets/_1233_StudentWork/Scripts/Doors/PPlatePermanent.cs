using System.Runtime.CompilerServices;
using UnityEngine;

public class PPlatePermanent : PressurePlateBase
{
    private bool _activated;

    private void OnTriggerEnter(Collider other) //if valid activator enters the trigger it opens the door
    {
        if (_activated) return;
        if (!IsValidActivator(other)) return;
        
        _activated = true;
        door.OpenDoor();
    }
}
