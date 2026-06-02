using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioSource _randomPitchSoundSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private float _lowPith = 0f;
    [SerializeField] private float _topPith = 2f;
    [SerializeField] private AudioClip _defaultBGMusic;
    [SerializeField] private float _sqrMaxDistanceToSource = 400f;

    private Transform _listenerTransform;
    private IPersistentProgressService _progress;

    private void OnDestroy() => 
        _progress.Progress.GameData.AudioChanged -= ResetVolume;

    public void Construct(IPersistentProgressService progress)
    {
        _progress = progress;

        _progress.Progress.GameData.AudioChanged += ResetVolume;

        SettingSources();
        ResetVolume();

        DontDestroyOnLoad(this);
    }

    public void SetListener(AudioListener listener) => 
        _listenerTransform = listener.transform;

    public bool CanBeHeard(Vector3 source) =>           
         (source - _listenerTransform.position).sqrMagnitude < _sqrMaxDistanceToSource;    

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip) => 
        _soundsSource.PlayOneShot(clip);

    public void PlayRandomPithSound(AudioClip clip)
    {
        _randomPitchSoundSource.pitch = Random.Range(_lowPith, _topPith);
        _randomPitchSoundSource.PlayOneShot(clip);
    }
    private void SettingSources()
    {
        _musicSource.playOnAwake = true;
        _musicSource.loop = true;

        PlayMusic(_defaultBGMusic);

        _soundsSource.loop = false;
        _soundsSource.playOnAwake = false;
    }

    private void ResetVolume()
    {
        _soundsSource.mute = _progress.Progress.GameData.IsSoundMute;
        _randomPitchSoundSource.mute = _progress.Progress.GameData.IsSoundMute;
        _musicSource.mute = _progress.Progress.GameData.IsMusicMute;

        _soundsSource.volume = _progress.Progress.GameData.SoundValue;
        _randomPitchSoundSource.volume = _progress.Progress.GameData.SoundValue;
        _musicSource.volume = _progress.Progress.GameData.MusicValue;
    }
}
