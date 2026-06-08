using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkillsWindow : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private Button _returnButton;
    [SerializeField] private ButtonPlaySoundOnInteract _returnButtonSound;

    public ButtonPlaySoundOnInteract ReturnButtonSound => _returnButtonSound;
    public Transform Content => _content;

    private void OnEnable() => 
        _returnButton.onClick.AddListener(Hide);

    private void OnDisable() =>
        _returnButton.onClick.RemoveListener(Hide);

    private void Start() => 
        Hide();

    public void Show()
    {
        YG2.InterstitialAdvShow();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        YG2.InterstitialAdvShow();
        gameObject.SetActive(false);
    }
}
