using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] private Transform _focusFrame;
    [SerializeField] private Image _levelImage;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _button;
    [SerializeField] private ButtonPlaySoundOnInteract _buttonSound;

    private IPersistentProgressService _progress;

    public SceneID LevelID { get; private set; }

    public event Action<LevelCard> Selected;

    public ButtonPlaySoundOnInteract Sound => _buttonSound;

    private void OnDestroy()
    {
        _progress.Progress.LevelsProgress.OpenedLevel -= OnOpenNewLevel;
        _button.onClick.RemoveListener(OnClickButton);
    }

    private void Start()
    {
        if (_progress.Progress.LevelsProgress.ReachedLevels.Contains(LevelID))
            EnableButton();
        else
            DisableButton();        

        Deselect();
    }

    public void Construct(string name, Sprite levelSprite, SceneID levelId, IPersistentProgressService progressService)
    {
        _name.text = name;
        LevelID = levelId;
        _levelImage.sprite = levelSprite;
        _progress = progressService;

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
        if(sceneID == LevelID)
            EnableButton();
    }

    private void EnableButton() => 
        _button.interactable = true;

    private void DisableButton() => 
        _button.interactable = false;
}