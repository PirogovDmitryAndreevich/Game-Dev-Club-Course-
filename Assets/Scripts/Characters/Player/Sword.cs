using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool IsAttack { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAttack && collision.TryGetComponent(out Enemy enemy))
        Debug.Log("Sword: hit");
    }

    public void Attack()
    {
        IsAttack = true;
    }
    public void StopAttack()
    {
        IsAttack = false;
    }
}
