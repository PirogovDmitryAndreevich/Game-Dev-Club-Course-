using DG.Tweening;
using System;
using UnityEngine;

public class FinishWithBusStop
{
    private const float BusSpeed = 5f;
    private const float PlayerSpeed = 5f;

    private readonly BusStop _busStop;
    private readonly Bus _bus;
    private readonly Player _player;

    public FinishWithBusStop(BusStop busStop, Bus bus, Player player)
    {
        _busStop = busStop;
        _bus = bus;
        _player = player;
    }

    public event Action CutsceneEnded;

    public void Play()
    {
        _player.StartFinishing();
        _player.CharacterAnimator.SetIsWalk(false);

        _bus.gameObject.SetActive(true);

        float distance = Mathf.Abs(_bus.transform.position.x - _busStop.transform.position.x);
        float duration = distance / BusSpeed;

        _bus.SetAnimation(true);

        _bus.transform.DOMoveX(_busStop.transform.position.x, duration)
            .SetEase(Ease.Linear)
            .Play()
            .OnComplete(PutPlayerOnBus);
    }

    private void PutPlayerOnBus()
    {
        _bus.SetAnimation(false);

        _player.CharacterMover.enabled = false;
        _player.CharacterAnimator.SetIsWalk(true);

        float distance = (_bus.transform.position - _player.transform.position).magnitude;
        float duration = distance / PlayerSpeed;

        _player.transform.DOMove(_bus.transform.position, duration)
            .SetEase(Ease.Linear)
            .Play()
            .OnComplete(BusGoOut);
    }

    private void BusGoOut()
    {
        _player.gameObject.SetActive(false);

        float distance = Mathf.Abs(_bus.EndMovePoint.x - _bus.transform.position.x);
        float duration = distance / BusSpeed;

        _bus.SetAnimation(true);

        _bus.transform.DOMoveX(_bus.EndMovePoint.x, duration)
            .SetEase(Ease.Linear)
            .Play()
            .OnComplete(EndingCutscene);

    }

    private void EndingCutscene()
    {
        _bus.gameObject.SetActive(false);
        CutsceneEnded?.Invoke();
    }
}
