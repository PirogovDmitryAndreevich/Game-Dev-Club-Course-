using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        _soundImage = _soundButton.GetComponent<Image>();
        _musicImage = _musicButton.GetComponent<Image>();
    }

    private void OnEnable()
    {
        _soundSlider.onValueChanged.AddListener(ChangedSoundVolume);
        _musicSlider.onValueChanged.AddListener(ChangedMusicVolume);

        _soundButton.onClick.AddListener(SwitchMuteSound);
        _musicButton.onClick.AddListener(SwitchMuteMusic);

        _playerData = SaveData.GameData;
        ResetSettings();
    }

    private void OnDisable()
    {
        _soundSlider.onValueChanged.RemoveListener(ChangedSoundVolume);
        _musicSlider.onValueChanged.RemoveListener(ChangedMusicVolume);

        _soundButton.onClick.RemoveListener(SwitchMuteSound);
        _musicButton.onClick.RemoveListener(SwitchMuteMusic);

        SaveData.Save();
    }

    private void ChangedSoundVolume(float value)
    {
        _playerData.SoundValue = value;

        if (value <= 0f)
        {
            if (!_playerData.IsSoundMute)
                _playerData.IsSoundMute = true;
        }
        else
        {
            if (_playerData.IsSoundMute)
                _playerData.IsSoundMute = false;
        }

        _soundImage.sprite = _playerData.IsSoundMute ? _soundOff : _soundOn;
    }

    private void ChangedMusicVolume(float value)
    {
        _playerData.MusicValue = value;

        if (value <= 0f)
        {
            if (!_playerData.IsMusicMute)
                _playerData.IsMusicMute = true;
        }
        else
        {
            if (_playerData.IsMusicMute)
                _playerData.IsMusicMute = false;
        }

        _musicImage.sprite = _playerData.IsMusicMute ? _musicOff : _musicOn;
    }

    private void SwitchMuteSound()
    {
        _playerData.IsSoundMute = !_playerData.IsSoundMute;
        SetSoundSprite();
        SaveData.Save();
    }

    private void SwitchMuteMusic()
    {
        _playerData.IsMusicMute = !_playerData.IsMusicMute;
        SetMusicSprite();
        SaveData.Save();
    }

    private void SetSoundSprite()
    {
        _soundImage.sprite = _playerData.IsSoundMute ? _soundOff : _soundOn;
    }

    private void SetMusicSprite()
    {
        _musicImage.sprite = _playerData.IsMusicMute ? _musicOff : _musicOn;
    }

    private void ResetSettings()
    {
        _soundSlider.SetValueWithoutNotify(_playerData.SoundValue);
        _musicSlider.SetValueWithoutNotify(_playerData.MusicValue);
        SetSoundSprite();
        SetMusicSprite();
    }
}
