using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using YG;

public class LoadSceneState : IPayloadState<SceneID>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IScenesLogicContainer _scenesContainer;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IPoolService _poolService;
    private IScene loadedScene;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
        IScenesLogicContainer scenesContainer, ICoroutineRunner coroutineRunner,
        IPoolService poolService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _scenesContainer = scenesContainer;
        _coroutineRunner = coroutineRunner;
        _poolService = poolService;
    }

    public void Enter(SceneID sceneId)
    {
        _curtain.Show();
        _poolService.Cleanup();
        loadedScene = _scenesContainer.Scenes[sceneId];
        _sceneLoader.Load(sceneId, OnLoaded);
    }

    public void Exit()
    {
        _curtain.Hide();        
    }

    private void OnLoaded()
    {
        loadedScene.InitGameObjects();
        SetLanguage();
        _gameStateMachine.Enter<GameLoopState>();
    }

    private void SetLanguage()
    {
        try
        {
            _coroutineRunner.StartCoroutine(LoadLocale(YG2.lang));
        }
        catch (System.Exception) { }
    }

    private IEnumerator LoadLocale(string languageIdentifier)
    {
        yield return LocalizationSettings.InitializationOperation;

        LocaleIdentifier localeCode = new LocaleIdentifier(languageIdentifier);

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            Locale locale = LocalizationSettings.AvailableLocales.Locales[i];

            if (locale.Identifier == localeCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                yield break;
            }
        }
    }
}
