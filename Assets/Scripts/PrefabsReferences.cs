using UnityEngine;

public class PrefabsReferences : MonoBehaviour
{
    public static PrefabsReferences Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] public  GameObject BombCircleArea;
}
