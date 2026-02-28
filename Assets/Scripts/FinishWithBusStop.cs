using DG.Tweening;
using System;
using UnityEngine;

public class FinishWithBusStop : MonoBehaviour
{
    private BusStop _busStop;
    private Bus _bus;
    private Player _player;
    private Vector2 _endBusPosition;
    private float _playerSpeed;
    private Tween _moveTween;

    public event Action CutsceneEnded;

    private void OnDestroy()
    {
        _moveTween?.Kill();

        if (_bus != null)
        {
            _bus.MovementCompleted -= PutPlayerOnBus;
            _bus.MovementCompleted -= EndingCutscene;
        }
    }

    public void SetCutscene(BusStop busStop, Bus bus, Player player, Vector2 endBusPosition ,float playerSpeed)
    {
        _busStop = busStop;
        _bus = bus; 
        _player = player;
        _playerSpeed = playerSpeed;
        _endBusPosition = endBusPosition;
    }

    public void Play()
    {
        _bus.StartToBusStop(_busStop.transform.position);
        _bus.MovementCompleted += PutPlayerOnBus;
    }

    private void PutPlayerOnBus()
    {
        _bus.MovementCompleted -= PutPlayerOnBus;
        _player.StartMovingInCutscene();

        float distance = (_bus.transform.position - _player.transform.position).magnitude;
        float duration = distance / _playerSpeed;

        _moveTween = _player.transform.DOMove(_bus.transform.position, duration)
            .SetEase(Ease.Linear)
            .Play()
            .OnComplete(() =>
            {
                _moveTween = null;
                _player.gameObject.SetActive(false);
                BusGoOut();
            }
                );
    }

    private void BusGoOut()
    {
        _bus.MovementCompleted += EndingCutscene;
        _bus.StartGoOut(_endBusPosition);

    }

    private void EndingCutscene()
    {
        _bus.MovementCompleted -= EndingCutscene;
        CutsceneEnded?.Invoke();
    }
}
