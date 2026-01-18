using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private Button _pauseButton;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OpenPauseWindow);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OpenPauseWindow);
    }

    private void OpenPauseWindow()
    {
        _pauseWindow.gameObject.SetActive(true);
        _pauseWindow.Show();
    }
}
