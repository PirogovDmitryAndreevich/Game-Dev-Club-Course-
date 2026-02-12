using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static bool IsLoaded { get; private set; }

    public static event Action OnLoaded;

    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioSource _randomPitchSoundSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private float _lowPith = 0f;
    [SerializeField] private float _topPith = 2f;
    [SerializeField] private AudioClip _defaultBGMusic;

    [SerializeField] private float _sqrMaxDistanceToSource = 400f;

    private AudioListener _listener;
    private GameSaveData _playerSave;
    private Transform _listenerTransform;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);

        _musicSource.playOnAwake = true;
        _musicSource.loop = true;

        PlayMusic(_defaultBGMusic);

        _soundsSource.loop = false;
        _soundsSource.playOnAwake = false;

        if (SaveData.IsLoaded)
            Init();        
        else        
            SaveData.OnLoaded += Init;

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Init()
    {
        SaveData.OnLoaded -= Init;

        _playerSave = SaveData.GameData;
        _playerSave.OnAudioChanged += ResetVolume;
        ResetVolume();
        OnLoaded?.Invoke();
        IsLoaded = true;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (_playerSave != null)
            _playerSave.OnAudioChanged -= ResetVolume;
    }

    public bool CanBeHeard(Vector3 source) =>           
         (source - _listenerTransform.position).sqrMagnitude < _sqrMaxDistanceToSource;
    

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        _soundsSource.PlayOneShot(clip);
    }

    public void PlayRandomPithSound(AudioClip clip)
    {
        _randomPitchSoundSource.pitch = UnityEngine.Random.Range(_lowPith, _topPith);
        _randomPitchSoundSource.PlayOneShot(clip);
    }

    private void ResetVolume()
    {
        _soundsSource.mute = _playerSave.IsSoundMute;
        _randomPitchSoundSource.mute = _playerSave.IsSoundMute;
        _musicSource.mute = _playerSave.IsMusicMute;

        _soundsSource.volume = _playerSave.SoundValue;
        _randomPitchSoundSource.volume = _playerSave.SoundValue;
        _musicSource.volume = _playerSave.MusicValue;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _listener = FindAnyObjectByType<AudioListener>();

        if (_listener != null)
            _listenerTransform = _listener.transform;
    }
}
