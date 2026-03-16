using UnityEngine;

public class PressurePlateBase : MonoBehaviour
{
    [SerializeField] protected Door door;

    protected bool IsValidActivator(Collider other)
    {
        return other.CompareTag("Player") || other.CompareTag("Box"); // the valid activators are only the player and box tags
    }
}
