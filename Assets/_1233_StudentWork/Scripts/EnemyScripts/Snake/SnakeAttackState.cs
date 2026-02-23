using UnityEngine;

public class SnakeAttackState : EnemyState
{
    private readonly SnakeBrain _brain;
    private float _exitTime;

    public SnakeAttackState(SnakeBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Enter()
    {
        //stop moving and trigger the attack animation
        _brain.Mover?.Stop();
        _brain.AnimatorDriver.SetSpeed(0);
        _brain.AnimatorDriver.TriggerAttack();

        //calculate when we can leave this state
        _exitTime = Time.time + _brain.AttackCooldown;

        //apply damage immediately (simplified)
        ApplyMeleeDamage();
    }

    public override void Tick()
    {
        var target = _brain.TargetProvider.GetTarget();
        var targetPos = _brain.TargetProvider.GetTargetPosition();

        if (target != null) _brain.Rotator.FacePosition(targetPos);


        // Return to chase state once the cooldown is over
        if (Time.time >= _exitTime) Machine.ChangeState(new SnakeChaseState(_brain, Machine));
    }
    
    private void ApplyMeleeDamage()
    {
        var target = _brain.TargetProvider.GetTarget();
        if (target == null) return;

        //final check to see if target is still in range
        if (Vector3.Distance(_brain.transform.position, target.position) <= _brain.AttackRange + 0.5f)
        {
            var receiver = target.GetComponent<IDamageReciever>();
            if (receiver != null)
            {
                receiver.ApplyDamage(
                    new DamageInfo
                    {
                        Amount = _brain.AttackDamage,
                        Source = _brain.gameObject,
                        HitPoint = target.position,
                        HitNormal = Vector3.up
                    }
                    );
            }
        }
    }
    

}
