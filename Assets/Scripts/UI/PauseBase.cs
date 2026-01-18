using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PauseBase : MonoBehaviour
{
    private const int MainMenuSceneIndex = 0;

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


}
