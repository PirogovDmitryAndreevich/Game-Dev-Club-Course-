using DG.Tweening;
using UnityEngine;

public class Defense : MonoBehaviour, IItem, IInteractable
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private Transform _view;
    [SerializeField] private int _value = 10;

    [Header("Idle animation")]
    [SerializeField] private float _floatHeight = 0.5f;
    [SerializeField] private float _duration = 1.5f;

    public int Value => _value;

    private Vector3 _startPos;
    private Player _player;

    private void OnEnable()
    {
        _startPos = _view.position;

        _view.DOMoveY(_startPos.y + _floatHeight, _duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .Play();
    }

    public void Interact()
    {
        _player.AddArmor(this);
        Collect();
    }

    public void Collect()
    {
        _view.DOKill();
        AudioManager.Instance.PlaySound(_sound);
        Destroy(gameObject);
    }

    public void HighlightOn()
    {
        throw new System.NotImplementedException();
    }

    public void HighlightOff()
    {
        throw new System.NotImplementedException();
    }

    public void CollisionEnter(Player player)
    {
        _player = player;
    }
}
