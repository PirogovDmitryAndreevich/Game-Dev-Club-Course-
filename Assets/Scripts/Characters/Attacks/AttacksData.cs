using System;
using System.Collections.Generic;
using UnityEngine;

public class AttacksData : MonoBehaviour
{
    public static AttacksData Instance { get; private set; }

    [SerializeField] private AttackBase[] _attacksArray;

    public static Dictionary<AttacksType, AttackBase> Attacks = new();

    public static event Action OnInitialized;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

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

        OnInitialized?.Invoke();
    }
}
