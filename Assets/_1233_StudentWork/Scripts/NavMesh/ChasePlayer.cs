using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private PlayerTargetProvider _targetProvider;
    [SerializeField] private NavMeshAgent _agent;

    //public getters for agent related info
    public Vector3 Velocity => _agent.velocity;
    public bool HasPath => _agent.hasPath;

    public void SetDestination(Vector3 targetPos)
    {
        targetPos = _targetProvider.GetTargetPosition();
        _agent.SetDestination(targetPos);
    }

    public void Stop()
    {
        _agent?.ResetPath();
    }
}
