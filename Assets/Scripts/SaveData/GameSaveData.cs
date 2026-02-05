using System;

[System.Serializable]
public class GameSaveData
{
    public float _soundValue = 1f;
    public float _musicValue = 1f;
    public bool _isSoundMute = false;
    public bool _isMusicMute = false;

    public Action OnAudioChanged;

    public float SoundVolume
    {
        get => _soundValue;
        set
        {
            if (_soundValue != value)
            {
                _soundValue = value;
                OnAudioChanged?.Invoke();
            }
        }
    }
    public float MusicVolume
    {
        get => _musicValue;
        set
        {
            if (_musicValue != value)
            {
                _musicValue = value;
                OnAudioChanged?.Invoke();
            }
        }
    }
    public bool IsSoundMute
    {
        get => _isSoundMute;
        set
        {
            if (_isSoundMute != value)
            {
                _isSoundMute = value;
                OnAudioChanged?.Invoke();
            }
        }
    }
    public bool IsMusicMute
    {
        get => _isMusicMute;
        set
        {
            if (_isMusicMute != value)
            {
                _isMusicMute = value;
                OnAudioChanged?.Invoke();
            }
        }
    }
}
