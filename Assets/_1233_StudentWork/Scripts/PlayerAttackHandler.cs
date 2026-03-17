using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    [SerializeField] SwordAttack _swordAttack;

    private void Attack()
    {
        _swordAttack.PerformAttack();
    }
}
