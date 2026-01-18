using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private FailWindow _failWindow;

    private void Awake()
    {
        _failWindow.Initialize(_player);
    }
}
