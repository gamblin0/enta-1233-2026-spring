using UnityEditor;
using UnityEngine;

public class BloomAttackState : EnemyState
{
    private readonly BloomBrain _brain;

    public BloomAttackState(BloomBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Enter()
    {
        //stop moving to shoot
        _brain.Mover?.Stop();
        _brain.AnimatorDriver.SetSpeed(0);
    }

    public override void Tick()
    {
        //1. check if we still have target
        var target = _brain.TargetProvider.GetTarget();
        var targetPos = _brain.TargetProvider.GetTargetPosition();
        if (target == null)
        {
            Machine.ChangeState(new BloomMoveState(_brain, Machine));
            return;
        }

        var distance = Vector3.Distance(_brain.transform.position, target.position);
        var hasLOS = _brain.Detection.HasLineOfSight(target);

        //2. if LOS is lost or we are out of range go back to move state
        if (hasLOS == false || distance > _brain.AttackRange)
        {
            Machine.ChangeState(new BloomMoveState(_brain, Machine));
            return;
        }

        //3. face the player and shoot if weapon is ready
        _brain.Rotator.FacePosition(targetPos);
        if (_brain.Weapon.CanFire)
        {
            _brain.AnimatorDriver.TriggerAttack();
            _brain.Weapon.Fire(targetPos);
        }

        //4. optional: if player gets too close, kite
        if (distance < _brain.StopRange - 1f)
        {
            //simple kite logic: move aaway from target
            var kiteDir = (_brain.transform.position - target.position).normalized;
            _brain.Mover?.SetDestination(_brain.transform.position + kiteDir * 2f);
        }
    }
}
