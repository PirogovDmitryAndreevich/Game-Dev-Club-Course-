using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public abstract class PauseBase : MonoBehaviour
{
    private const int MainMenuSceneIndex = 0;

    [Header("Scene Settings")]
    [SerializeField] protected AudioManager CurrentAudioManager;

    protected Sequence Animation;

    protected bool IsAnimating => Animation != null && Animation.active;

    private void OnEnable() =>
        Enable();

    private void OnDisable() =>
        Disable();    

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

    protected virtual void Enable()
    {
        TimeManager.Pause();
    }

    protected virtual void Disable()
    {
        TimeManager.Run();
    }

    protected void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    protected void KillCurrentAnimationIfActive()
    {
        if (IsAnimating)
            Animation.Kill();
    }
}
