using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _damageText;


    private void SetDamage(int damage)
    {
        _damageText.text = damage.ToString();
    }
}
