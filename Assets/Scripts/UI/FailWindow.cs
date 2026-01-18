using System;
using UnityEngine;
using UnityEngine.UI;

public class FailWindow : PauseBase
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private Player _player;

    protected override void OnEnable()
    {
        base.OnEnable();
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
        _player.OnPlayerDied -= OnPlayerDied;
    }

    public override void Show()
    {
        throw new NotImplementedException();
    }

    public override void Hide(Action callback)
    {
        throw new NotImplementedException();
    }    

    internal void Initialize(Player player)
    {
        _player = player;
        _player.OnPlayerDied += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        gameObject.SetActive(true);
        Show();
    }
}
