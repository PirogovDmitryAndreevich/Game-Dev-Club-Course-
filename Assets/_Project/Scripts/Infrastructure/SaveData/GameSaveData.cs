using System;

[Serializable]
public class GameSaveData
{
    public float SoundValue;
    public float MusicValue;
    public bool IsSoundMute;
    public bool IsMusicMute;

    public event Action AudioChanged;

    public GameSaveData()
    {
        SoundValue = 1f;
        MusicValue = 1f;
        IsSoundMute = false;
        IsMusicMute = false;
    }

    public void SwitchMusicMute(bool isMute)
    {
        IsMusicMute = isMute;
        AudioChanged?.Invoke();
    }

    public void SwitchSoundMute(bool isMute)
    {
        IsSoundMute = isMute;
        AudioChanged?.Invoke();
    }

    public void ChangeMusicVolume(float value)
    {
        MusicValue = value;
        AudioChanged?.Invoke();
    }

    public void ChangeSoundVolume(float value)
    {
        SoundValue = value;
        AudioChanged?.Invoke();
    }
}
