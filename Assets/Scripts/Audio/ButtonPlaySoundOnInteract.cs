using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPlaySoundOnInteract : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private AudioClip _pointerDown;
    [SerializeField] private AudioClip _pointerEnter;
    [SerializeField] private AudioClip _pointerExit;
    [SerializeField] private AudioClip _pointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        PlaySound(_pointerDown);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(_pointerEnter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            PlaySound(_pointerExit);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
            PlaySound(_pointerUp);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            AudioManager.Instance.PlaySound(clip);
    }
}
