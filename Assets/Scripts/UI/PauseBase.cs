using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public abstract class PauseBase : MonoBehaviour
{
    private const int MainMenuSceneIndex = 0;

    protected Sequence _animation;

    protected bool IsAnimating => _animation != null && _animation.active;

    protected virtual void OnEnable()
    {
        TimeManager.Pause();
    }

    protected virtual void OnDisable()
    {
        TimeManager.Run();
    }

    public abstract void Show();

    public abstract void Hide(Action callback);

    protected virtual void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected virtual void Continue()
    {
        Hide(() => gameObject.SetActive(false));
    }

    protected virtual void Exit()
    {
        LoadScene(MainMenuSceneIndex);
    }

    protected void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    protected void KillCurrentAnimationIfActive()
    {
        if (IsAnimating)
            _animation.Kill();
    }
}
