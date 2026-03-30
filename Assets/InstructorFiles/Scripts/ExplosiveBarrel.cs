using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ExplosiveBarrel : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRadius = 4f;
    [SerializeField] private LayerMask detectionLayers;

    [Header("Explosion")]
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 600f;
    [SerializeField] private int explosionDamage = 60;

    [Header("VFX")]
    [SerializeField] private GameObject explosionEffect;

    private bool _exploded;

    private void Update()
    {
        DetectTarget();
    }

    private void DetectTarget()
    {
        if (_exploded) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayers);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            if (hit.TryGetComponent<IDamageReciever>(out _))
            {
                Explode();
                return;
            }
        }
    }

    private void Explode()
    {
        if (_exploded) return;
        _exploded = true;

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            Vector3 direction = (hit.transform.position - transform.position).normalized;

            DamageInfo info = new DamageInfo
            {
                Amount = explosionDamage,
                Source = gameObject,
                HitPoint = hit.ClosestPoint(transform.position),
                HitNormal = direction
            };

            if (hit.TryGetComponent<IDamageReciever>(out var receiver))
            {
                receiver.ApplyDamage(info);
            }

            if (hit.TryGetComponent<Rigidbody>(out var body))
            {
                body.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = new Color(1f, 0.4f, 0f, 0.4f);
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
