using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class WinWindow : PauseBase
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private float _appearanceDuration = 0.5f;
    [SerializeField] private float _appearanceButton = 0.5f;

    private CanvasGroup _canvasGroup;
    private int _nextSceneIndex;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
         _nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;
        _nextButton.interactable = SceneManager.sceneCountInBuildSettings > _nextSceneIndex;

        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
        _nextButton.onClick.AddListener(NextLevel);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
        _nextButton.onClick.RemoveListener(NextLevel);
    }

    public override void Show()
    {
        KillCurrentAnimationIfActive();

        _animation = DOTween.Sequence();

        _animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(1f, _appearanceDuration)).SetEase(Ease.Flash)
            .Append(_restartButton.transform.DOScale(1f, _appearanceButton).From(0f).SetEase(Ease.OutBounce))
            .Join(_exitButton.transform.DOScale(1f, _appearanceButton).From(0f).SetEase(Ease.OutBounce))
            .Play();
    }

    public override void Hide(Action callback)
    {
        KillCurrentAnimationIfActive();

        _animation = DOTween.Sequence();

        _animation
            .SetUpdate(true)
            .Append(_exitButton.transform.DOScale(0f, _appearanceButton).From(1f).SetEase(Ease.OutBounce))
            .Join(_restartButton.transform.DOScale(0f, _appearanceButton).From(1f).SetEase(Ease.OutBounce))
            .Append(_canvasGroup.DOFade(0f, _appearanceDuration)).SetEase(Ease.Flash)
            .Play()
            .OnComplete(() => callback?.Invoke());
    }

    private void NextLevel()
    {
        LoadScene(_nextSceneIndex);
    }
}
