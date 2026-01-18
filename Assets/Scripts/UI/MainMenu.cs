using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const int DefaultLevelIndex = 1;

    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(DefaultLevelIndex);
    }
}
