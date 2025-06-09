using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Sword _sword;

    public bool CanAttack => _sword.IsAttack == false;

    public void Attack()
    {
       _sword.Attack();
    }

    internal void StopAttack()
    {
        _sword.StopAttack();
    }
}
