using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private AudioManager _currentAudioManager;
    [SerializeField] private Player _player;
    [SerializeField] private AudioClip _gameMusic;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private WinWindow _winWindow;
    [SerializeField] private FinishLevel _finisher;

    private void Awake()
    {
        _failWindow.Initialize(_player);

        if (_currentAudioManager.IsLoaded)
            InitMusic();
        else
            _currentAudioManager.Loaded += InitMusic;

        _finisher.FinishLevelConditionsCompleted += FinishLevelConditionsCompleted;
    }

    private void OnDestroy()
    {
       _finisher.FinishLevelConditionsCompleted -= FinishLevelConditionsCompleted;
    }

    private void InitMusic()
    {
        _currentAudioManager.Loaded -= InitMusic;

        _currentAudioManager.PlayMusic(_gameMusic);
    }

    private void FinishLevelConditionsCompleted()
    {
        _player.FinishLevelConditionsCompleted();
    }
}
