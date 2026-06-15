using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timer;

    public void UpdateTimer(float time)
    {
        int totalSeconds = Mathf.Max(0, Mathf.CeilToInt(time));

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        string timerText = string.Format("{0:00}:{1:00}", minutes, seconds);

        _timer.text = timerText;
    }
}