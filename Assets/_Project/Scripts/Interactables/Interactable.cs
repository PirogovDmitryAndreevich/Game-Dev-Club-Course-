using UnityEngine;
using DG.Tweening;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected Transform View;
    [SerializeField] private float _floatHeight = 0.5f;
    [SerializeField] private float _duration = 1.5f;

    protected Tween Animation;
    private Vector3 _startPos;

    private void Start() => 
        Moving();

    private void OnDestroy() =>
        Animation.Kill();

    protected virtual void Moving()
    {
        _startPos = View.position;

        Animation = View.DOMoveY(_startPos.y + _floatHeight, _duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .Play();
    }
}
