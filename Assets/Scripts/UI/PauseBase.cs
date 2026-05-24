using System;
using UnityEngine;
using DG.Tweening;

public abstract class PauseBase : MonoBehaviour
{
    protected AudioHandler CurrentAudioManager;
    protected GameStateMachine GameStateMachine;
    protected Sequence Animation;

    protected SceneID CurrentScene { get; set; }

    private bool IsAnimating =>
        Animation != null && Animation.active;

    private void OnEnable() =>
        Enable();

    private void OnDisable() =>
        Disable();

    private void Start() =>
        gameObject.SetActive(false);

    public abstract void Show();

    public abstract void Hide(Action callback);

    protected virtual void Restart() => 
        LoadScene(CurrentScene);

    protected virtual void Continue() => 
        Hide(() => gameObject.SetActive(false));

    protected virtual void Exit() => 
        LoadScene(SceneID.MainMenu);

    protected virtual void Enable() => 
        TimeManager.Pause();

    protected virtual void Disable() => 
        TimeManager.Run();

    protected void LoadScene(SceneID index) => 
        GameStateMachine.Enter<LoadSceneState, SceneID>(index);

    protected void KillCurrentAnimationIfActive()
    {
        if (IsAnimating)
            Animation.Kill();
    }
}
