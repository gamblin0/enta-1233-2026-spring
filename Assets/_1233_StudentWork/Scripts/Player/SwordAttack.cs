using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [Header("Attack Settings")] //this is just for the UI of Unity

    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float attackAngle = 60f;
    [SerializeField] private int damage = 30;
    [SerializeField] private LayerMask enemies;

    [SerializeField] private Transform attackOrigin;

    public void PerformAttack()
    {
        Collider[] hits = Physics.OverlapSphere(attackOrigin.position, attackRadius, enemies);

        foreach (Collider hit in hits )
        {
            Vector3 directionToTarget = (hit.transform.position - attackOrigin.position).normalized;

            float angle = Vector3.Angle(attackOrigin.forward, directionToTarget);

            if (angle <= attackAngle * 0.5f)
            {
                IDamageReciever damageReciever = hit.GetComponent<IDamageReciever>();

                if (damageReciever != null )
                {
                    DamageInfo info = new DamageInfo
                    {
                        Amount = damage,
                        Source = gameObject,
                        HitPoint = hit.ClosestPoint(attackOrigin.position),
                        HitNormal = -directionToTarget
                    };
                    damageReciever.ApplyDamage(info);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackOrigin == null) return;
        

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);

        Vector3 left = Quaternion.Euler(0, -attackAngle / 2, 0) * attackOrigin.forward;
        Vector3 right = Quaternion.Euler(0, attackAngle / 2, 0) * attackOrigin.forward;

        Gizmos.DrawRay(attackOrigin.position, left * attackRadius);
        Gizmos.DrawRay(attackOrigin.position, right * attackRadius);
    }
}
