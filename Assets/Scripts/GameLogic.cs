using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AudioClip _gameMusic;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private WinWindow _winWindow;
    //[SerializeField] private FinishLevel _finisher;

    private void Awake()
    {
        _failWindow.Initialize(_player);

        if (AudioManager.IsLoaded)
            InitMusic();
        else
            AudioManager.OnLoaded += InitMusic;

        //_finisher.OnLevelFinished += ShowWinWindow;
    }

    private void OnDestroy()
    {
       // _finisher.OnLevelFinished -= ShowWinWindow;
    }

    private void InitMusic()
    {
        AudioManager.OnLoaded -= InitMusic;

        AudioManager.Instance.PlayMusic(_gameMusic);
    }

    private void ShowWinWindow()
    {
        _winWindow.gameObject.SetActive(true);
        _winWindow.Show();
    }
}
