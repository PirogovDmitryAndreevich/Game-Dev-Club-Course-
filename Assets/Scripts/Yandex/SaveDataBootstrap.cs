using UnityEngine;
using YG;

public class SaveDataBootstrap : MonoBehaviour
{
    private static bool _exists;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (_exists)
            return;

        var go = new GameObject("SaveDataBootstrap");
        go.AddComponent<SaveDataBootstrap>();
    }

    private void Awake()
    {
        if (_exists)
        {
            Destroy(gameObject);
            return;
        }

        _exists = true;
        DontDestroyOnLoad(gameObject);

        YG2.onGetSDKData += OnYGLoaded;
    }

#if UNITY_EDITOR
    private void Start()
    {
        OnYGLoaded();
    }

#endif

    private void OnDestroy()
    {
        if (_exists)
            YG2.onGetSDKData -= OnYGLoaded;
    }

    private void OnYGLoaded()
    {
        SaveData.Load();
    }
}
