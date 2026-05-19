using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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

    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void LoadScene() => 
        _gameStateMachine.Enter<LoadSceneState, SceneID>(_firstLevelForLoad);

    private void ShowSettings()
    {
        _settingsWindow.gameObject.SetActive(true);
        _settingsWindow.Show();
    }
}
