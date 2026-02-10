using UnityEngine;

public class TargetProvider : MonoBehaviour
{
   public interface ITargetProvider
    {
        bool HasTarget { get; }
        Transform GetTarget();
        Vector3 GetTargetPosition();
    }
}
