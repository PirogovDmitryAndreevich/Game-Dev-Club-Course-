using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private AudioClip _gameMusic;

    private void Awake()
    {
        _failWindow.Initialize(_player);

        if (AudioManager.IsLoaded)
            InitMusic();
        else
            AudioManager.OnLoaded += InitMusic;
    }

    private void InitMusic()
    {
        AudioManager.OnLoaded -= InitMusic;

        AudioManager.Instance.PlayMusic(_gameMusic);
    }
}
