using DG.Tweening;
using UnityEngine;

public class Coin : Interactable
{
    [SerializeField] private float _flyDistance = 1.5f;
    [SerializeField] private float _flyDuration = 0.35f;
    [SerializeField] private int _reward = 100;

    private ISaveLoadService _save;
    private IPersistentProgressService _progress;
    private IPoolService _pool;
    private Sequence seq;

    private void OnDisable()
    {
        Animation.Kill();
        seq.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            Collect();
    }

    public void Construct(IPoolService poolService, IPersistentProgressService progress, ISaveLoadService save)
    {
        _save = save;
        _progress = progress;
        _pool = poolService;
    }

    private void Collect()
    {
        _progress.Progress.PlayerData.SetStat(StatsType.Coins, _reward);
        _save.SaveProgress();
        _pool.Return(this);
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
        Animation?.Kill();

        Vector2 randomDir = Random.insideUnitCircle.normalized;

        Vector3 targetPos = transform.position +
                            (Vector3)(randomDir * _flyDistance);

        seq = DOTween.Sequence();

        seq.Append(
            transform.DOScale(1f, 0.2f).From(0f)
                .SetEase(Ease.OutBack)
        );

        seq.Join(
            transform.DOMove(targetPos, _flyDuration)
                .SetEase(Ease.OutQuad)
        );

        seq.Join(
            transform.DORotate(
                new Vector3(0, 0, Random.Range(-180, 180)),
                _flyDuration
            )
        );

        seq.Play();

        seq.OnComplete(() =>
        {
            seq.Kill();
            Moving();
        });
    }
}