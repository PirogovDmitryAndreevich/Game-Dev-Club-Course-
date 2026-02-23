using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScaleOnInteract : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Scale Settings")]
    [SerializeField] private float hoverScale = 1.04f;
    [SerializeField] private float pressedScale = 0.97f;

    [Header("Animation")]
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private Ease ease = Ease.OutQuad;

    private RectTransform rectTransform;
    private Tween scaleTween;
    private Vector3 initialScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialScale = rectTransform.localScale;
    }

    private void OnDisable()
    {
        scaleTween?.Kill();
    }    

    public void OnPointerEnter(PointerEventData eventData)
    {
        ScaleTo(hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleTo(1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ScaleTo(pressedScale);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ScaleTo(hoverScale);
    }

    private void ScaleTo(float scale)
    {
        scaleTween?.Kill();
        scaleTween = rectTransform
            .DOScale(initialScale * scale, duration)
            .SetEase(ease)
            .Play();
    }
}
