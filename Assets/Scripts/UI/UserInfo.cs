using TMPro;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;

    private AttackBase _defaultPlayerAttack;
    private void Start()
    {
        if (AttacksData.Instance != null && AttacksData.Attacks.Count != 0)
            Init();
        else
            AttacksData.OnInitialized += Init;
    }

    private void OnDisable()
    {
        _defaultPlayerAttack.OnDamageChanged -= SetDamage;
    }

    private void Init()
    {
        AttacksData.OnInitialized -= Init;

        _defaultPlayerAttack =
            AttacksData.Attacks[AttacksType.PlayerDefaultAttack];

        _defaultPlayerAttack.OnDamageChanged += SetDamage;
        SetDamage(_defaultPlayerAttack.Damage);
    }

    private void SetDamage(int damage)
    {
        _damageText.text = damage.ToString();
    }
}
