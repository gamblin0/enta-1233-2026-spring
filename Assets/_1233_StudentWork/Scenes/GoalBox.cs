using UnityEngine;

public class GoalBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMgr.Instance.NextLevel();
        }
    }
}
