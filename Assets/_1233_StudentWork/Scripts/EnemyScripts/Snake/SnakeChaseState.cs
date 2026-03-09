using UnityEngine;

public class SnakeChaseState : EnemyState
{
    private readonly SnakeBrain _brain;

    public SnakeChaseState(SnakeBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Tick()
    {
        //1. get player position
        var target = _brain.TargetProvider.GetTarget();
        if (target == null || !_brain.Detection.IsTargetInDetectionRange(target))
        {
            Machine.ChangeState(new SnakeIdleState(_brain, Machine));
            return;
        }

        //2. tell mover to go there
        _brain.Mover?.SetDestination(target.position);

        //3. update anims based on movemennt speed
        
        _brain.AnimatorDriver.SetSpeed(_brain.Mover?.Velocity.magnitude ?? 0f);
        

        //4. if we are close enough, switch to atttack state
        var distance = Vector3.Distance(_brain.transform.position, target.position);
        
        if (distance <= _brain.AttackRange)
        {
            Debug.Log("switching");
            Machine.ChangeState(new SnakeAttackState(_brain, Machine));
        }
        
       
    }
}
