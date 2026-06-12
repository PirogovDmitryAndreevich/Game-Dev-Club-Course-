using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.Pay;

public class InAppButton : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _price;

    private void OnEnable() =>
        _button.onClick.AddListener(BuyPurchase);

    private void OnDisable() =>
        _button.onClick.RemoveListener(BuyPurchase);

    private void Start() => 
        UpdateEntries();

    public void BuyPurchase() =>
        YG2.BuyPayments(_id);

    private void UpdateEntries()
    {
        Purchase coinsData = YG2.PurchaseByID(_id);
        _price.text = coinsData.priceValue;
    }
}