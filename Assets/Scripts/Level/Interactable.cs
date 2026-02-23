using UnityEngine;
using DG.Tweening;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Transform _view;
    [SerializeField] private float _floatHeight = 0.5f;
    [SerializeField] private float _duration = 1.5f;

    private Vector3 _startPos;

    protected virtual void Moving()
    {
        _startPos = _view.position;

        _view.DOMoveY(_startPos.y + _floatHeight, _duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .Play();
    }

    protected virtual void OnDisable()
    {
        _view.DOKill();
    }
}
