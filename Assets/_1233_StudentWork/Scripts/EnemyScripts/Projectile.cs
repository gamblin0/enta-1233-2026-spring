using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private bool _useGravity;

    private Rigidbody _rb;
    private GameObject _source;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = _useGravity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //dont hit source
        if (collision.gameObject == _source) return;

        //check if we hit something damageable
        var damageReciever = collision.gameObject.GetComponentInParent<IDamageReciever>();
        if (damageReciever != null)
        {
            var info = new DamageInfo
            {
                Amount = _damage,
                Source = _source,
                HitPoint = collision.contacts[0].point,
                HitNormal = collision.contacts[0].normal,
            };
            damageReciever.ApplyDamage(info);
        }

        //destroy on impact
        Destroy(gameObject);
    }

    public void Launch(Vector3 direction, GameObject source)
    {
        _source = source;
        _rb.linearVelocity = direction.normalized * _speed;
        transform.forward = direction;
        Destroy(gameObject, _lifetime); //simple destruction for now
    }

    public void LaunchWithVelocity(Vector3 velocity, GameObject source)
    {
        _source = source;
        _rb.linearVelocity = velocity;
        if (velocity.sqrMagnitude > 0.001f)        
            transform.forward = velocity;
        _rb.useGravity = true; //force gravity for arc shots
        Destroy(gameObject, _lifetime);

        
    }
}
