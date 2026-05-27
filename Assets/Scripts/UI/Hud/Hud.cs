using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private UserInfo _userInfo;
    [SerializeField] private Inventory _inventoryView;
    [SerializeField] private Button _pauseButton;

    private PauseWindow _pauseWindow;

    public HealthBar HealthBar => _healthBar;
    public UserInfo UserInfo => _userInfo;
    public Inventory Inventory => _inventoryView;

    private void OnDestroy()
    {
        _pauseButton.onClick.RemoveListener(OpenPauseWindow);
    }
    
    public void Construct(PauseWindow pauseWindow)
    {
        _pauseWindow = pauseWindow;

        _pauseButton.onClick.AddListener(OpenPauseWindow);
    }

    private void OpenPauseWindow()
    {
        _pauseWindow.gameObject.SetActive(true);
        _pauseWindow.Show();
    }
}
