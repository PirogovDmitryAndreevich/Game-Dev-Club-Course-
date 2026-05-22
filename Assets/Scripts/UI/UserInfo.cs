using TMPro;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    [Header("Scene settings")]
    [SerializeField] private AttacksData _attacksData;

    [SerializeField] private TMP_Text _damageText;

    private AttackBase _defaultPlayerAttack;
    private void Start()
    {
        if (_attacksData != null && _attacksData.Attacks.Count != 0)
            Init();
        else
            _attacksData.Initialized += Init;
    }

    private void OnDisable()
    {
        _defaultPlayerAttack.DamageChanged -= SetDamage;
    }

    private void Init()
    {
        _attacksData.Initialized -= Init;

        /*_defaultPlayerAttack =
            _attacksData.Attacks[AttacksType.PlayerDefaultAttack];*/

        _defaultPlayerAttack.DamageChanged += SetDamage;
        SetDamage(_defaultPlayerAttack.Damage);
    }

    private void SetDamage(int damage)
    {
        _damageText.text = damage.ToString();
    }
}
