using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] private Transform _focusFrame;
    [SerializeField] private Image _levelImage;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _trophyCount;
    [SerializeField] private Button _button;
    [SerializeField] private ButtonPlaySoundOnInteract _buttonSound;
    [SerializeField] private GameObject _trophyFrame;

    private IPersistentProgressService _progress;
    private LevelSaveData _levelSave;
    private bool _isEnable;
    private LevelData _levelData;

    public SceneID LevelID { get; private set; }

    public event Action<LevelCard> Selected;

    public ButtonPlaySoundOnInteract Sound => _buttonSound;

    private void OnDestroy()
    {
        _progress.Progress.LevelsProgress.OpenedLevel -= OnOpenNewLevel;
        _button.onClick.RemoveListener(OnClickButton);

        if(_isEnable)
            _levelSave.TrophyChanged -= UpdateTrophyCount;
    }

    private void Start()
    {
        if (_progress.Progress.LevelsProgress.Contain(LevelID))
            EnableButton();
        else
            DisableButton();

        Deselect();
    }

    public void Construct(LevelData levelData, IPersistentProgressService progressService)
    {
        _levelData = levelData;
        _progress = progressService;

        _name.text = _levelData.Name;
        LevelID = _levelData.ID;
        _levelImage.sprite = _levelData.Sprite;

        _progress.Progress.LevelsProgress.OpenedLevel += OnOpenNewLevel;
        _button.onClick.AddListener(OnClickButton);
    }

    public void Select() =>
        _focusFrame.gameObject.SetActive(true);

    public void Deselect() =>
        _focusFrame.gameObject.SetActive(false);

    private void OnClickButton()
    {
        _focusFrame.gameObject.SetActive(true);
        Selected?.Invoke(this);
    }

    private void OnOpenNewLevel(SceneID sceneID)
    {
        if (sceneID == LevelID)
            EnableButton();
    }

    private void EnableButton()
    {
        _isEnable = true;

        _levelSave = _progress.Progress.LevelsProgress.GetSaveData(LevelID);
        _trophyFrame.gameObject.SetActive(true);

        _button.interactable = true;

        _levelSave.TrophyChanged += UpdateTrophyCount;
        UpdateTrophyCount(LevelID);
    }

    private void DisableButton()
    {
        _isEnable = false;
        _trophyFrame.gameObject.SetActive(false);
        _button.interactable = false;
    }

    private void UpdateTrophyCount(SceneID id)
    {
        if (id == LevelID)
            _trophyCount.text = $"{_levelSave.ReachedTrophy} / {_levelSave.Trophy}";
    }
}