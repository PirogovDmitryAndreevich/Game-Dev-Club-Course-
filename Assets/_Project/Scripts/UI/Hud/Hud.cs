using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private StatusBar _statusBar;
    [SerializeField] private UserInfo _userInfo;
    [SerializeField] private Inventory _inventoryView;
    [SerializeField] private IndicateInteractButton _indicate;
    [SerializeField] private TasksView _tasksView;
    [SerializeField] private Button _pauseButton;

    private PauseWindow _pauseWindow;
    private Player _player;

    public HealthBar HealthBar => _healthBar;
    public UserInfo UserInfo => _userInfo;
    public Inventory Inventory => _inventoryView;
    public TasksView TaskView => _tasksView; 
    public StatusBar StatusBar => _statusBar;

    private void OnDestroy()
    {
        _player.WasSetInteractable -= Interactable;
        _pauseButton.onClick.RemoveListener(OpenPauseWindow);
    }
    
    public void Construct(PauseWindow pauseWindow, Player player)
    {
        _pauseWindow = pauseWindow;
        _player = player;

        _player.WasSetInteractable += Interactable;
        _pauseButton.onClick.AddListener(OpenPauseWindow);

        Interactable();
    }

    private void OpenPauseWindow()
    {
        _pauseWindow.gameObject.SetActive(true);
        _pauseWindow.Show();
    }

    private void Interactable()
    {
        _indicate.Show(_player.Interactable != null);
    }
}
