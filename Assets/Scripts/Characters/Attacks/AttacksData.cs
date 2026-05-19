using System;
using System.Collections.Generic;
using UnityEngine;

public class AttacksData : MonoBehaviour
{
    [SerializeField] private AttackBase[] _attacksArray;

    public bool IsInitialized;

    public Dictionary<AttacksType, AttackBase> Attacks = new();

    public event Action Initialized;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        Attacks.Clear();

        foreach (var attack in _attacksArray)
        {
            if (attack == null)
                continue;

            if (!Attacks.ContainsKey(attack.Type))
                Attacks.Add(attack.Type, attack);
        }

        IsInitialized = true;
        Initialized?.Invoke();
    }
}
