using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _returnButton;
    [SerializeField] private AudioClip _hideShowSound;
    [SerializeField] private float _appearanceDuration = 0.5f;

    [Header("Buttons")]
    [SerializeField] private DayRewardButton[] _rewardDayButtons;
    [SerializeField] private ShopUpdateButton _defenseButton;
    [SerializeField] private ShopUpdateButton _healthButton;

    private Sequence Animation;

    private IPersistentProgressService _progress;
    private ISaveLoadService _save;

    private AudioHandler _audioHandler { get; set; }

    private ShopStaticData _staticData;

    private bool IsAnimating =>
        Animation != null && Animation.active;

    private void OnEnable()
    {
        _defenseButton.ButtonPressed += OnUpdateDefenseClick;
        _healthButton.ButtonPressed += OnUpdateHealthClick;
        _returnButton.onClick.AddListener(Hide);

        YG2.onPurchaseSuccess += SuccessPurchased;
        YG2.onPurchaseFailed += FailedPurchased;
    }

    private void OnDisable()
    {
        _defenseButton.ButtonPressed -= OnUpdateDefenseClick;
        _healthButton.ButtonPressed -= OnUpdateHealthClick;
        _returnButton.onClick.RemoveListener(Hide);

        YG2.onPurchaseSuccess -= SuccessPurchased;
        YG2.onPurchaseFailed -= FailedPurchased;
    }

    private void Start() =>
        gameObject.SetActive(false);

    public void Construct(AudioHandler handler, ShopStaticData staticData,IPersistentProgressService progressService, ISaveLoadService save)
    {
        _audioHandler = handler;
        _staticData = staticData;
        _progress = progressService;
        _save = save;

        _defenseButton.Initialize(_staticData.DefensePrice, _staticData.DefenseValue);
        _healthButton.Initialize(
            _staticData.HitPointsPrice * _progress.Progress.PlayerData.HitPointUpdateCount,
            _staticData.HitPointsValue);
    }

    public void Hide()
    {
        KillCurrentAnimationIfActive();

        _audioHandler.PlaySound(_hideShowSound);

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

        _audioHandler.PlaySound(_hideShowSound);

        gameObject.SetActive(true);

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(1f, _appearanceDuration)).SetEase(Ease.Flash)
            .Play();
    }

    private void OnUpdateDefenseClick()
    {
        int price = _staticData.DefensePrice;

        if (_progress.Progress.PlayerData.Gems < price)
            return;

        int value = _staticData.DefenseValue;
        _progress.Progress.PlayerData.SetStat(StatsType.Gem, -price);
        _progress.Progress.PlayerData.SetStat(StatsType.Defense, value);
        _save.SaveProgress();

        _defenseButton.UpdateView(price, value);
    }

    private void OnUpdateHealthClick()
    {
        int price = _staticData.HitPointsPrice * _progress.Progress.PlayerData.HitPointUpdateCount;

        if (_progress.Progress.PlayerData.Gems < price)
            return;

        int value = _staticData.HitPointsValue;
        _progress.Progress.PlayerData.SetStat(StatsType.Gem, -price);
        _progress.Progress.PlayerData.SetStat(StatsType.Health, value);
        _save.SaveProgress();

        _healthButton.UpdateView(
            _staticData.HitPointsPrice * _progress.Progress.PlayerData.HitPointUpdateCount,
            _staticData.HitPointsValue);
    }

    private void SuccessPurchased(string id)
    {
        if (id == "Coins")
        {
            UnityEngine.Debug.Log("Coins In App");
        }
        else if (id == "Gems")
        {
            UnityEngine.Debug.Log("Gems In App");
        }
        else if (id == "Mix")
        {
            UnityEngine.Debug.Log("Mix In App");
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
