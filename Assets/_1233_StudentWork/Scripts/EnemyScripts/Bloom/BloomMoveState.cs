using UnityEngine;

public class BloomMoveState : EnemyState
{
    private readonly BloomBrain _brain;

    public BloomMoveState(BloomBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Tick()
    {
        //1.get player position
        var target = _brain.TargetProvider.GetTarget();
        if (target == null) return;

        var distance = Vector3.Distance(_brain.transform.position, target.position);
        var hasLOS = _brain.Detection.HasLineOfSight(target);

        //2. if we have los in range switch to attack state
        if (hasLOS && distance <= _brain.AttackRange)
        {
            Machine.ChangeState(new BloomAttackState(_brain, Machine));
            return;
        }

        //3. move towards target to regain los or get in range
        _brain.Mover?.SetDestination(target.position);

        //4. update animations based on movement speed
        if (_brain.Mover != null)
            _brain.AnimatorDriver.SetSpeed(_brain.Mover.Velocity.magnitude);
        else
            _brain.AnimatorDriver.SetSpeed(0);
    }
}
