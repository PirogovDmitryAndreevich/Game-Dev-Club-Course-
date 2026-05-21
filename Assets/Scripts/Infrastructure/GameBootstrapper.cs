using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain _curtainPreafb;

    private Game _game;

    private void Awake()
    {
        _game = new Game(this, Instantiate(_curtainPreafb));
        _game.StateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}
