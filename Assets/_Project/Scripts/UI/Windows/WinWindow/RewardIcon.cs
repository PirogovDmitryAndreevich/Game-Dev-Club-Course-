using TMPro;
using UnityEngine;

public class RewardIcon : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetText(float value) =>
        _text.text = value.ToString();
}