using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageTest : MonoBehaviour
{
    [SerializeField] private AttacksType _type;
    [SerializeField] private int _plusDamage;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickButton);
    }

    private void OnClickButton()
    {
        AttacksData.Attacks[_type].Damage += _plusDamage;
    }
}
