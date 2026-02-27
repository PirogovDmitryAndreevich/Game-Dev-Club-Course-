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
        _bus.BusStopped += PutPlayerOnBus;
    }

    private void PutPlayerOnBus()
    {
        _bus.BusStopped -= PutPlayerOnBus;
        _player.StartMovingInCutscene();

        Vector2 finalTarget = _bus.transform.position;

        float distance = Mathf.Abs(transform.position.x - finalTarget.x);
        float duration = distance / _playerSpeed;


        _moveTween = _player.transform.DOMove(_bus.transform.position, duration)
            .SetEase(Ease.Linear)
            .Play()
            .OnComplete(() =>
            {
                _moveTween.Kill();
                _player.gameObject.SetActive(false);
                BusGoOut();
            }
                );
    }

    private void BusGoOut()
    {
        _bus.BusStopped += EndingCutscene;
        _bus.StartGoOut(_endBusPosition);

    }

    private void EndingCutscene()
    {
        _bus.BusStopped -= EndingCutscene;
        CutsceneEnded?.Invoke();
        Debug.Log("Finish cutscene is ended");
    }
}
