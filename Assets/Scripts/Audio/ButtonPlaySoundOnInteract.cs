using UnityEngine;
using UnityEngine.EventSystems;
using static YG.YG2;

public class ButtonPlaySoundOnInteract : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Clips")]
    [SerializeField] private AudioClip _pointerDown;
    [SerializeField] private AudioClip _pointerEnter;
    [SerializeField] private AudioClip _pointerExit;
    [SerializeField] private AudioClip _pointerUp;

    private AudioHandler _audioManger;

    public void Construct(AudioHandler audio)
    {
        _audioManger = audio;
        Debug.Log($"ButtonSound {gameObject.name} - {_audioManger != null}");
    }

    public void OnPointerDown(PointerEventData eventData) => 
        PlaySound(_pointerDown);

    public void OnPointerEnter(PointerEventData eventData) => 
        PlaySound(_pointerEnter);

    public void OnPointerExit(PointerEventData eventData) => 
        PlaySound(_pointerExit);

    public void OnPointerUp(PointerEventData eventData) => 
        PlaySound(_pointerUp);

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            _audioManger.PlaySound(clip);
    }
}
