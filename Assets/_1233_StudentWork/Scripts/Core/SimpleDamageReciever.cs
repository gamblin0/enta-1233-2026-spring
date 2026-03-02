using UnityEngine;

public class SimpleDamageReciever : MonoBehaviour, IDamageReciever
{
    [SerializeField] private Health _health;
    [SerializeField] private float _damageMultiplier = 1f;

    private void Awake()
    {
        if (_health == null) _health = GetComponent<Health>();
    }

    public void ApplyDamage(DamageInfo info)
    {
        if (_health == null) return;

        //appy multiplier (e.g. for weak points)
        info.Amount = Mathf.RoundToInt(info.Amount * _damageMultiplier);

        _health.ApplyDamage(info);
    }
}
