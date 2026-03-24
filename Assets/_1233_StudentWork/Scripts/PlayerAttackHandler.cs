using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    [SerializeField] SwordAttack _swordAttack;
    [SerializeField] PlayerController _player;

    private void Attack()
    {
        _swordAttack.PerformAttack();
        
    }
}
