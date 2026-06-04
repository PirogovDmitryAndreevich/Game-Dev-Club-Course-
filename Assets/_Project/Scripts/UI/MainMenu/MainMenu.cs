using UnityEngine;
using UnityEngine.UI;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Stat[] _stats;
    [SerializeField] private LeaderBoard _leaderboard;
    [SerializeField] private ButtonPlaySoundOnInteract[] _buttonPlaySounds; 
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private SceneID _firstLevelForLoad;

    [Header("Popups")]
    [SerializeField] private SettingsWidow _settingsWindow;
    [SerializeField] private SelectLevelWindow _levelSelector;
    [SerializeField] private ShopWindow _shopWindow;

    public LeaderBoard LeaderBoard => _leaderboard;
    public SelectLevelWindow LevelSelector => _levelSelector;
    public SettingsWidow Settings => _settingsWindow;
    public ShopWindow ShopWindow => _shopWindow;    

    private void OnEnable()
    {
        _startButton.onClick.AddListener(ShowLevelSelecter);
        _settingsButton.onClick.AddListener(ShowSettings);
        _shopButton.onClick.AddListener(ShowShop);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(ShowLevelSelecter);
        _settingsButton.onClick.RemoveListener(ShowSettings);
        _shopButton.onClick.RemoveListener(ShowShop);
    }

    public void Construct(IPersistentProgressService progress, IHandlersContainer handlers)
    {
        _levelSelector.gameObject.SetActive(false);

        foreach (Stat stat in _stats)
            stat.Construct(progress);

        foreach (ButtonPlaySoundOnInteract button in _buttonPlaySounds)
            button.Construct(handlers.Audio);
    }

    private void ShowLevelSelecter()
    {
        YG2.InterstitialAdvShow();
        _levelSelector.gameObject.SetActive(true);
        _levelSelector.Show();
    }

    private void ShowSettings()
    {
        YG2.InterstitialAdvShow();
        Settings.gameObject.SetActive(true);
        Settings.Show();
    }

    private void ShowShop()
    {
        YG2.InterstitialAdvShow();
        ShopWindow.gameObject.SetActive(true);
        ShopWindow.Show();
    }
}
