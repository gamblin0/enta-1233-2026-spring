using UnityEngine;

public class SetAsPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMgr.Instance.DebugAssignAsPlayer(gameObject);
    }

    
}
