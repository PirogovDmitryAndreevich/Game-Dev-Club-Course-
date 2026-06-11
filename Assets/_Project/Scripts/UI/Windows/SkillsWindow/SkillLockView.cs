using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillLockView : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buyButton;
    [SerializeField] private ButtonPlaySoundOnInteract _sound;

    public event Action BuyButtonPressed;

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnBuyClick);
    }

    public void Construct(AudioHandler audio, int price)
    {
        _price.text = price.ToString();

        _sound.Construct(audio);

        _buyButton.onClick.AddListener(OnBuyClick);
    }

    public void Show() => 
        _view.SetActive(true);

    public void Hide() => 
        _view.SetActive(false);

    private void OnBuyClick() => 
        BuyButtonPressed?.Invoke();
}
