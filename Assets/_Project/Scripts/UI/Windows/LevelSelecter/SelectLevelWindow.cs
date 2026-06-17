using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SelectLevelWindow : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private Button _startButton;
    [SerializeField] private ButtonPlaySoundOnInteract _startButtonSound;
    [SerializeField] private Button _returnButton;
    [SerializeField] private ButtonPlaySoundOnInteract _returnButtonSound;
    [SerializeField] private ArenaLevelCard _arenaLevelCard;

    private IStaticData _staticData;
    private AudioHandler _audio;
    private GameStateMachine _stateMachine;

    private ILevelCard _currentSelected;

    private Dictionary<SceneID, ILevelCard> _cards = new Dictionary<SceneID, ILevelCard>();

    public ButtonPlaySoundOnInteract StartButtonSound => _startButtonSound;
    public ButtonPlaySoundOnInteract ReturnButtonSound => _returnButtonSound;
    public ArenaLevelCard ArenaLevelCard => _arenaLevelCard;

    private void OnDestroy()
    {
        foreach(var card in _cards)
            card.Value.Selected -= OnSelectCard;

        _arenaLevelCard.Selected -= OnSelectCard;

        _returnButton.onClick.RemoveListener(Hide);
        _startButton.onClick.RemoveListener(OnStartButton);
    }

    private void Start() =>
        gameObject.SetActive(false);

    public void Construct(GameStateMachine stateMachine, AudioHandler audio, IStaticData staticData, IUIFactory factory)
    {
        _audio = audio;
        _stateMachine = stateMachine;
        _staticData = staticData;

        foreach (var level in _staticData.LevelsData)
        {
            if (level.Key == SceneID.Arena)
                continue;

            var card = factory.CreateLevelCard(level.Value, _content);
            _cards.Add(card.LevelID, card);

            card.Selected += OnSelectCard;
        }

        _cards.Add(SceneID.Arena, _arenaLevelCard);
        _arenaLevelCard.Selected += OnSelectCard;

        _startButton.onClick.AddListener(OnStartButton);
        _returnButton.onClick.AddListener(Hide);
    }

    public void Show()
    {
        YG2.InterstitialAdvShow();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        YG2.InterstitialAdvShow();
        gameObject.SetActive(false);
    }

    private void OnSelectCard(ILevelCard card)
    {
        if (_currentSelected != null)
            _currentSelected.Deselect();

        _currentSelected = card;
        _currentSelected.Select();
    }

    private void OnStartButton()
    {
        if (_currentSelected == null)
            return;

        YG2.InterstitialAdvShow();

        _stateMachine.Enter<LoadSceneState, SceneID>(_currentSelected.LevelID);
    }
}
