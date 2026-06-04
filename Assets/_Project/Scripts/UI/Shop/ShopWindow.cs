using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _appearanceDuration = 0.5f;

    [Header("Buttons")]
    [SerializeField] private DayRewardButton[] _rewardDayButtons;
    [SerializeField] private Button _damageUpdate;
    [SerializeField] private Button _defenseUpdate;
    [SerializeField] private Button _healthUpdate;

    private Sequence Animation;

    private AudioHandler _audioHandler { get; set; }
    private bool IsAnimating =>
        Animation != null && Animation.active;

    private void OnEnable()
    {
        _damageUpdate.onClick.AddListener(OnUpdateDamageClick);
        _defenseUpdate.onClick.AddListener(OnUpdateDefenseClick);
        _healthUpdate.onClick.AddListener(OnUpdateHealthClick);

        YG2.onPurchaseSuccess += SuccessPurchased;
        YG2.onPurchaseFailed += FailedPurchased;
    }

    private void OnDisable()
    {
        _damageUpdate.onClick.RemoveListener(OnUpdateDamageClick);
        _defenseUpdate.onClick.RemoveListener(OnUpdateDefenseClick);
        _healthUpdate.onClick.RemoveListener(OnUpdateHealthClick);

        YG2.onPurchaseSuccess -= SuccessPurchased;
        YG2.onPurchaseFailed -= FailedPurchased;
    }

    private void Start() =>
        gameObject.SetActive(false);

    public void Construct(AudioHandler handler)
    {
        _audioHandler = handler;
    }

    public void Hide()
    {
        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(0f, _appearanceDuration)).SetEase(Ease.Flash)
            .Play()
            .OnComplete(() => gameObject.SetActive(false));
    }

    public void Show()
    {
        KillCurrentAnimationIfActive();

        gameObject.SetActive(true);

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(1f, _appearanceDuration)).SetEase(Ease.Flash)
            .Play();
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

    private void SuccessPurchased(string id)
    {
        if (id == "Coins")
        {
            Debug.Log("Coins In App");
        }
        else if (id == "Gems")
        {
            Debug.Log("Gems In App");
        }
        else if (id == "Mix")
        {
            Debug.Log("Mix In App");
        }
    }

    private void FailedPurchased(string id)
    {

    }

    private void KillCurrentAnimationIfActive()
    {
        if (IsAnimating)
            Animation.Kill();
    }
}
