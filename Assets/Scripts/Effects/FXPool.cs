using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPool : MonoBehaviour
{
    [SerializeField] private FXEntry[] fxList;

    [System.Serializable]
    public class FXEntry
    {
        public FXType type;
        public FXBase prefab;
        public int preloadCount;
    }

    private Dictionary<FXType, Queue<FXBase>> pools = new();

    private void Start()
    {
        StartCoroutine(Preload());
    }

    public FXBase Get(FXType type)
    {
        var pool = pools[type];
        int count = pool.Count;

        for (int i = 0; i < count; i++)
        {
            var fx = pool.Dequeue();

            if (!fx.IsActive)
            {
                fx.gameObject.SetActive(true);
                fx.IsActive = true;
                return fx;
            }

            pool.Enqueue(fx);
        }

        var entry = System.Array.Find(fxList, e => e.type == type);
        var created = Create(entry);
        created.gameObject.SetActive(true);

        return created;
    }

    public void Return(FXBase fx)
    {
        ((MonoBehaviour)fx).gameObject.SetActive(false);
        pools[fx.Type].Enqueue(fx);
        fx.IsActive = false;
    }

    private FXBase Create(FXEntry entry)
    {
        FXBase fx = Instantiate((MonoBehaviour)entry.prefab, transform)
            as FXBase;

        ((MonoBehaviour)fx).gameObject.SetActive(false);
        pools[entry.type].Enqueue(fx);
        return fx;
    }

    private IEnumerator Preload()
    {
        foreach (var entry in fxList)
        {
            pools[entry.type] = new Queue<FXBase>();

            for (int i = 0; i < entry.preloadCount; i++)
            {
                Create(entry);
                yield return null;
            }
        }
    }
}
