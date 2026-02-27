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
    [SerializeField] private BusStop _finalBusStop;
    [SerializeField] private Bus _bus;
    [SerializeField] private FinishWithBusStop _busStopFinisher;
    [SerializeField] private float _playerSpeed;

    private void Awake()
    {
        _failWindow.Initialize(_player);

        _finalBusStop.Interacted += StartFinishProcess;

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

    private void StartFinishProcess()
    {        
        var bus = Instantiate(_bus, _finalBusStop.BusSpawnPoint.position, Quaternion.identity);
        _busStopFinisher.SetCutscene(_finalBusStop, bus, _player, _finalBusStop.BusEndedPoint.position, _playerSpeed);
        _busStopFinisher.Play();
    }

}
