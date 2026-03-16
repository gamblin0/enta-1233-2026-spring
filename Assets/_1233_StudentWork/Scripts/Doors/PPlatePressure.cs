using UnityEngine;

public class PPlatePressure : PressurePlateBase
{
    private int _objectsOnPlate = 0;

    private void OnTriggerEnter(Collider other) // when the player or the box enters the trigger, it opens the door and reports that there is something on the plate
    {
        if (!IsValidActivator(other)) return;

        _objectsOnPlate++;
        door.OpenDoor();
    }

    private void OnTriggerExit(Collider other) // when the player or the box leaves the trigger, the door closes and the activator leaving gets reported
    {
        if (!IsValidActivator(other)) return;

        _objectsOnPlate--;

        if (_objectsOnPlate <= 0)
        {
            door.CloseDoor();
        }
    }
}
