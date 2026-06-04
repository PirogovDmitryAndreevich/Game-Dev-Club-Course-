using UnityEngine;
using UnityEngine.UI;
using YG;

public class InAppButton : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private Button _button;

    private void OnEnable() =>
        _button.onClick.AddListener(BuyPurchase);

    private void OnDisable() =>
        _button.onClick.RemoveListener(BuyPurchase);

    public void BuyPurchase() =>
        YG2.BuyPayments(_id);
}