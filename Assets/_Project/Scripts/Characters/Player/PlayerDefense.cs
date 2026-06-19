using System;

public class PlayerDefense
{
    private const int ArmorReductionInOneHit = 1;

    private PlayerSaveData _progress;

    public PlayerDefense(IPersistentProgressService progressService) => 
        _progress = progressService.Progress.PlayerData;

    public event Action DefenseChanged;
    public event Action AddedDefense;

    public bool IsShield => Defense > 0;
    public int Defense => _progress.GetStat(StatsType.Defense);    

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
        _progress.SetStat(StatsType.Defense, value);
        DefenseChanged?.Invoke();
    }
}