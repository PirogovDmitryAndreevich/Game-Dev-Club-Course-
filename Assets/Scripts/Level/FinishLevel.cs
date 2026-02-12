using System;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FinishLevel : MonoBehaviour, IInteractable
{
    private Animator _animator;
    private bool _isOpen;

    [SerializeField] private BusStop[] _interactObjects;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private TMP_Text _diedEnemyCounterText;

    public event Action OnLevelFinished;
    private int _allEnemy = 0;
    private int _diedEnemy = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _allEnemy = _enemies.Length;

        foreach (Enemy enemy in _enemies)
            enemy.OnCharacterDied += EnemyDied;
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in _enemies)
            enemy.OnCharacterDied -= EnemyDied;
    }

    public void Interact()
    {
        if (_interactObjects.All(i => i.IsActivated == true) && _isOpen == false)
        {
            _animator.SetTrigger(ConstantsData.AnimatorParameters.IsOpen);
            _isOpen = true;
            OnLevelFinished?.Invoke();
        }
        else
        {
            Debug.Log("Not all runes are activated");
        }
    }

    public void HighlightOn()
    {
        Debug.Log("[Finish] highlight on");
    }

    public void HighlightOff()
    {
        Debug.Log("[Finish] highlight off");
    }

    private void EnemyDied()
    {
        _diedEnemy++;
        UpdateEnemyCounterView();
        CheckFinished();
    }

    private void UpdateEnemyCounterView()
    {
        _diedEnemyCounterText.text = $"{_diedEnemy} / {_allEnemy}";
    }

    private void CheckFinished()
    {
        if (_diedEnemy == _allEnemy)
            OnLevelFinished?.Invoke();
    }
}
