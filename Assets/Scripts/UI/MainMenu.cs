using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const int DefaultLevelIndex = 1;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;

    [Header("Popups")]
    [SerializeField] private SettingsWidow _settingsWindow;

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

    private void LoadScene()
    {
        SceneManager.LoadScene(DefaultLevelIndex);
    }

    private void ShowSettings()
    {
        _settingsWindow.gameObject.SetActive(true);
        _settingsWindow.Show();
    }
}
