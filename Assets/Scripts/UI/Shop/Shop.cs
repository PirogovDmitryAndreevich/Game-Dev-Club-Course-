using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Shop : PauseBase
{
    [Header("Buttons")]
    [SerializeField] private DayRewardButton[] _rewardDayButtons;
    [SerializeField] private Button _damageUpdate;
    [SerializeField] private Button _defenseUpdate;
    [SerializeField] private Button _healthUpdate;

    [Header("InApp buttons")]
    [SerializeField] private Button _mixInApp;
    [SerializeField] private Button _coinsInApp;
    [SerializeField] private Button _gemsInApp;

    [SerializeField] private float _appearanceDuration = 0.5f;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void Hide(Action callback)
    {
        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(0f, _appearanceDuration)).SetEase(Ease.Flash)
            .Play()
            .OnComplete(() => callback?.Invoke());
    }

    public override void Show()
    {
        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(1f, _appearanceDuration)).SetEase(Ease.Flash)
            .Play();
    }

    protected override void Enable()
    {
        _damageUpdate.onClick.AddListener(OnUpdateDamageClick);
        _defenseUpdate.onClick.AddListener(OnUpdateDefenseClick);
        _healthUpdate.onClick.AddListener(OnUpdateHealthClick);
    }

    protected override void Disable()
    {
        _damageUpdate.onClick.RemoveListener(OnUpdateDamageClick);
        _defenseUpdate.onClick.RemoveListener(OnUpdateDefenseClick);
        _healthUpdate.onClick.RemoveListener(OnUpdateHealthClick);
    }

    private void OnUpdateDamageClick()
    {
        Debug.Log("Update Damage click");
    }

    private void OnUpdateDefenseClick()
    {
        Debug.Log("Update Defense click");
    }

    private void OnUpdateHealthClick()
    {
        Debug.Log("Update Health click");
    }
}
