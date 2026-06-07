using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillLockView : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buyButton;
    [SerializeField] private ButtonPlaySoundOnInteract _sound;

    private PlayerAttackType _type;

    private PlayerAttacksData _progress;
    private ISaveLoadService _save;

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnBuyClick);
    }

    public void Construct(PlayerAttackType type, PlayerAttacksData attackSaveData, ISaveLoadService save, AudioHandler audio)
    {
        _type = type;
        _progress = attackSaveData;
        _save = save;

        _sound.Construct(audio);

        _buyButton.onClick.AddListener(OnBuyClick);
    }

    public void Show() => 
        _view.SetActive(true);

    public void Hide() => 
        _view.SetActive(false);

    private void OnBuyClick()
    {
        _progress.OpenNewAttacks(_type);
        _save.SaveProgress();
    }
}
