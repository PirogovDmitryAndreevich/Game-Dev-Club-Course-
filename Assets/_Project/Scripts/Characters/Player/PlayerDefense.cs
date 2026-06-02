using System;
using UnityEngine;

public class PlayerDefense
{
    private const int ArmorReductionInOneHit = 1;

    private readonly IPersistentProgressService _progressService;

    public PlayerDefense(IPersistentProgressService progressService)
    {
        _progressService = progressService;

        Defense = _progressService.Progress.PlayerData.Defense;
        IsShield = Defense > 0;
    }

    public event Action DefenseChanged;
    public event Action AddedDefense;

    public bool IsShield { get; private set; }
    public int Defense { get; private set; }    

    public void AddDefense(int value)
    {
        if (value <= 0)
            return;

        AddedDefense?.Invoke();

        ChangeDefense(value);
    }

    public void SpendDefense() => 
        ChangeDefense(-ArmorReductionInOneHit);

    private void ChangeDefense(int value)
    {
        Defense = Mathf.Max(0, Defense + value);
        IsShield = Defense > 0;

        DefenseChanged?.Invoke();
    }
}