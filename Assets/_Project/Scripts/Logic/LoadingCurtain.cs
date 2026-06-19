using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCurtain : MonoBehaviour
{
    private const float MaxLoadingProgress = 0.85f;
    private const float HideDelay = 0.3f;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private float _speed = 1.5f;

    private Coroutine _animation;
    private float _targetValue;

    private void Awake() =>
        DontDestroyOnLoad(gameObject);

    public void Show()
    {
        gameObject.SetActive(true);
        _slider.value = 0;
        _targetValue = 0;

        UpdateProgressText(0);

        if (_animation != null)
            StopCoroutine(_animation);

        _animation = StartCoroutine(AnimateLoading());
    }

    public void SetProgress(float value) =>
        _targetValue = value;

    public void Hide()
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _animation = StartCoroutine(FillAndHide());
    }

    private IEnumerator AnimateLoading()
    {
        while (true)
        {
            _slider.value = Mathf.MoveTowards(
                _slider.value,
                _targetValue,
                _speed * Time.deltaTime);

            UpdateProgressText(_slider.value);

            yield return null;
        }
    }

    private IEnumerator FillAndHide()
    {
        while (_slider.value < 1f)
        {
            _slider.value = Mathf.MoveTowards(
                _slider.value,
                1f,
                _speed * Time.deltaTime);

            UpdateProgressText(_slider.value);

            yield return null;
        }

        UpdateProgressText(1f);

        yield return new WaitForSeconds(HideDelay);

        gameObject.SetActive(false);
    }

    private void UpdateProgressText(float progress)
    {
        int percent = Mathf.RoundToInt(progress * 100f);
        _progressText.text = $"{percent}%";
    }
}
