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

    private GameStateMachine _gameStateMachine;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(LoadScene);
        _settingsButton.onClick.AddListener(ShowSettings);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadScene);
        _settingsButton.onClick.RemoveListener(ShowSettings);
    }

    public void Construct(GameStateMachine gameStateMachine, IPersistentProgressService progress, IHandlersContainer handlers, ISaveLoadService save)
    {
        _gameStateMachine = gameStateMachine;        

        _settingsWindow.Construct(handlers.Audio);
        _settingsWindow.AudioSlider.Construct(progress, save);

        foreach (Stat stat in _stats)
            stat.Construct(progress);

        foreach (ButtonPlaySoundOnInteract button in _buttonPlaySounds)
            button.Construct(handlers.Audio);
    }

    private void LoadScene() => 
        _gameStateMachine.Enter<LoadSceneState, SceneID>(_firstLevelForLoad);

    private void ShowSettings()
    {
        _settingsWindow.gameObject.SetActive(true);
        _settingsWindow.Show();
    }
}
