using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTextAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _scaleMultiplayer;

    public void UpdateText(int value)
    {
        Vector2 originalScale = _text.transform.localScale;

        Sequence animation = DOTween.Sequence();

        animation
            .SetUpdate(true)
            .Append(
            _text.transform.DOScale(_text.transform.localScale * _scaleMultiplayer, 0.3f)
            .SetEase(Ease.OutBounce))
            .AppendCallback(() =>
            {
                _text.text = value.ToString();
            })
            .Append(_text.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBounce))
            .Play();
    }
}