using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] private Transform _focusFrame;
    [SerializeField] private Image _levelImage;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _button;

    public SceneID LevelID { get; private set; }

    public event Action<LevelCard> Selected;

    private void OnDestroy() => 
        _button.onClick.RemoveListener(OnClickButton);

    private void Start() =>
        Deselect();

    public void Construct(string name, Sprite levelSprite, SceneID levelId)
    {
        _name.text = name;
        LevelID = levelId;
        _levelImage.sprite = levelSprite;

        _button.onClick.AddListener(OnClickButton);
    }

    public void Select() =>
        _focusFrame.gameObject.SetActive(true);

    public void Deselect() =>
        _focusFrame.gameObject.SetActive(false);

    private void OnClickButton()
    {
        _focusFrame.gameObject.SetActive(true);
        Selected?.Invoke(this);
    }
}