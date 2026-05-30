using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TasksView : MonoBehaviour
{
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private Image _view;
    [SerializeField] private Color _reachedColor;

    public void UpdateCounter(int diedEnemies, int taskEnemies) => 
        _counter.text = $"{diedEnemies}/{taskEnemies}";

    public void Reached()
    {
        _view.DOColor(_reachedColor, 0.5f)
            .Play()
            .SetEase(Ease.OutBounce);
    }
}
