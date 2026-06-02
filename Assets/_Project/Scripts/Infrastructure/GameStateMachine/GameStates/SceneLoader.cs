using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) =>
        _coroutineRunner = coroutineRunner;

    public void Load(SceneID nextScene, Action onLoad = null) =>
        _coroutineRunner.StartCoroutine(LoadScene(nextScene, onLoad));

    private IEnumerator LoadScene(SceneID nextScene, Action onLoad = null)
    {
        string nextSceneName = nextScene.ToString();

        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextSceneName);

        while (!waitNextScene.isDone)
            yield return null;

        onLoad?.Invoke();
    }
}