using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    //[SerializeField] private HealthBar _healthBar;
    //[SerializeField] private Button _pauseButton;

    private PauseWindow _pauseWindow;   

    private void OpenPauseWindow()
    {
        _pauseWindow.gameObject.SetActive(true);
        _pauseWindow.Show();
    }
}
