using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Stat[] _stats;
    [SerializeField] private ButtonPlaySoundOnInteract[] _buttonPlaySounds; 
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private SceneID _firstLevelForLoad;

    [Header("Popups")]
    [SerializeField] private SettingsWidow _settingsWindow;
    [SerializeField] private SelectLevelWindow _levelSelector;

    public SelectLevelWindow LevelSelector => _levelSelector;
    public SettingsWidow Settings => _settingsWindow;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(ShowLevelSelecter);
        _settingsButton.onClick.AddListener(ShowSettings);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(ShowLevelSelecter);
        _settingsButton.onClick.RemoveListener(ShowSettings);
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
        _levelSelector.gameObject.SetActive(true);
    }

    private void ShowSettings()
    {
        Settings.gameObject.SetActive(true);
        Settings.Show();
    }
}
