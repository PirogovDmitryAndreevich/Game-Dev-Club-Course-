using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaLevelCard : MonoBehaviour, ILevelCard
{
    [SerializeField] private GameObject _focusFrame;
    [SerializeField] private TMP_Text _recordTime;
    [SerializeField] private Button _button;
    [SerializeField] private ButtonPlaySoundOnInteract _buttonSound;
    [SerializeField] private GameObject _recordFrame;

    private IPersistentProgressService _progress;
    private ArenaSaveData _saveData;

    public SceneID LevelID => SceneID.Arena;

    public event Action<ILevelCard> Selected;

    public ButtonPlaySoundOnInteract Sound => _buttonSound;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClickButton);

        _progress.Progress.LevelsProgress.ArenaSave.RecordChanged -= OnRecordChanged;
    }

    private void Start()
    {
        if (_progress.Progress.LevelsProgress.ArenaSave.IsThereARecord)
            OnRecordChanged();
        else
            _recordFrame.SetActive(false);
    }

    public void Construct(IPersistentProgressService progressService)
    {
        _progress = progressService;
        _button.onClick.AddListener(OnClickButton);

        _progress.Progress.LevelsProgress.ArenaSave.RecordChanged += OnRecordChanged;
    }

    private void OnRecordChanged()
    {
        _recordFrame.SetActive(true);
        UpdateTimer(_progress.Progress.LevelsProgress.ArenaSave.RecordTime);
    }

    public void Deselect() => 
        _focusFrame.gameObject.SetActive(false);

    public void Select() => 
        _focusFrame.gameObject.SetActive(true);

    private void OnClickButton()
    {
        _focusFrame.gameObject.SetActive(true);
        Selected?.Invoke(this);
    }

    public void UpdateTimer(float time)
    {
        int totalSeconds = Mathf.Max(0, Mathf.CeilToInt(time));

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        string timerText = string.Format("{0:00}:{1:00}", minutes, seconds);

        _recordTime.text = timerText;
    }
}