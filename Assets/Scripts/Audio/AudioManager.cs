using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioListener _listener;

    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioSource _randomPitchSoundSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private float _lowPith = 0f;
    [SerializeField] private float _topPith = 2f;
    [SerializeField] private AudioClip _defaultBGMusic;
    [SerializeField] private float _sqrMaxDistanceToSource = 400f;

    private GameSaveData _playerSave;
    private Transform _listenerTransform;

    public event Action Loaded;

    public bool IsLoaded { get; private set; }

    private void Awake()
    {
        _musicSource.playOnAwake = true;
        _musicSource.loop = true;

        PlayMusic(_defaultBGMusic);

        _soundsSource.loop = false;
        _soundsSource.playOnAwake = false;

        if (SaveData.IsLoaded)
            Init();        
        else        
            SaveData.Loaded += Init;

        _listenerTransform = _listener.transform;
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

    private void Init()
    {
        SaveData.Loaded -= Init;

        _playerSave = SaveData.GameData;
        _playerSave.OnAudioChanged += ResetVolume;
        ResetVolume();
        Loaded?.Invoke();
        IsLoaded = true;
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
}
