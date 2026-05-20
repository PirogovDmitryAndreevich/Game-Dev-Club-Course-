using UnityEngine;
using UnityEngine.UI;
using static YG.YG2;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;

    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    [SerializeField] private Sprite _musicOn;
    [SerializeField] private Sprite _musicOff;

    private Image _soundImage;
    private Image _musicImage;

    private GameSaveData _playerData;
    private ISaveLoadService _save;

    private void Awake()
    {
        _soundImage = _soundButton.GetComponent<Image>();
        _musicImage = _musicButton.GetComponent<Image>();
    }

    private void OnDestroy()
    {
        _soundSlider.onValueChanged.RemoveListener(ChangedSoundVolume);
        _musicSlider.onValueChanged.RemoveListener(ChangedMusicVolume);

        _soundButton.onClick.RemoveListener(SwitchMuteSound);
        _musicButton.onClick.RemoveListener(SwitchMuteMusic);

        _save.SaveProgress();
    }

    public void Construct(IPersistentProgressService progressService, ISaveLoadService save)
    {
        _playerData = progressService.Progress.GameData;
        _save = save;

        _soundSlider.onValueChanged.AddListener(ChangedSoundVolume);
        _musicSlider.onValueChanged.AddListener(ChangedMusicVolume);

        _soundButton.onClick.AddListener(SwitchMuteSound);
        _musicButton.onClick.AddListener(SwitchMuteMusic);

        ResetSettings();
    }

    private void ChangedSoundVolume(float value)
    {
        _playerData.ChangeSoundVolume(value);

        if (value <= 0f)
        {
            if (!_playerData.IsSoundMute)
                _playerData.SwitchSoundMute(true);
        }
        else
        {
            if (_playerData.IsSoundMute)
                _playerData.SwitchSoundMute(false);
        }

        _soundImage.sprite = _playerData.IsSoundMute ? _soundOff : _soundOn;
    }

    private void ChangedMusicVolume(float value)
    {
        _playerData.ChangeMusicVolume(value);

        if (value <= 0f)
        {
            if (!_playerData.IsMusicMute)
                _playerData.SwitchMusicMute(true);
        }
        else
        {
            if (_playerData.IsMusicMute)
                _playerData.SwitchMusicMute(false);
        }

        _musicImage.sprite = _playerData.IsMusicMute ? _musicOff : _musicOn;
    }

    private void SwitchMuteSound()
    {
        _playerData.SwitchSoundMute(!_playerData.IsSoundMute);
        SetSoundSprite();
        _save.SaveProgress();
    }

    private void SwitchMuteMusic()
    {
        _playerData.SwitchMusicMute(!_playerData.IsMusicMute);
        SetMusicSprite();
        _save.SaveProgress();
    }

    private void SetSoundSprite() => 
        _soundImage.sprite =
        _playerData.IsSoundMute
        ? _soundOff
        : _soundOn;

    private void SetMusicSprite() => 
        _musicImage.sprite = 
        _playerData.IsMusicMute
        ? _musicOff 
        : _musicOn;

    private void ResetSettings()
    {
        _soundSlider.SetValueWithoutNotify(_playerData.SoundValue);
        _musicSlider.SetValueWithoutNotify(_playerData.MusicValue);
        SetSoundSprite();
        SetMusicSprite();
    }
}
